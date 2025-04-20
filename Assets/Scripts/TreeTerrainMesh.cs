using UnityEngine;
using System.Collections.Generic;

public class TreeTerrainMesh : MonoBehaviour
{
    public Terrain terrain; // Assigner dans l'inspecteur
    public GameObject parentObject; // Optionnel

    [Range(0.01f, 1f)]
    public float lodTransitionHeight = 0.4f; // Hauteur de transition LOD0 → LOD1

    void Start()
    {
        CombineTreesWithLOD();
    }

    void CombineTreesWithLOD()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain non assigné.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] treeInstances = terrainData.treeInstances;
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        Dictionary<int, List<CombineInstance>> treeMeshes = new Dictionary<int, List<CombineInstance>>();
        Dictionary<int, List<Vector3>> treePositions = new Dictionary<int, List<Vector3>>();

        for (int i = 0; i < treeInstances.Length; i++)
        {
            TreeInstance tree = treeInstances[i];
            int prototypeIndex = tree.prototypeIndex;
            GameObject prefab = treePrototypes[prototypeIndex].prefab;

            if (prefab == null) continue;

            MeshFilter meshFilter = prefab.GetComponentInChildren<MeshFilter>();
            if (meshFilter == null) continue;

            Mesh mesh = meshFilter.sharedMesh;
            Vector3 worldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            Matrix4x4 matrix = Matrix4x4.TRS(
                worldPos,
                Quaternion.Euler(0, tree.rotation * Mathf.Rad2Deg, 0),
                new Vector3(tree.widthScale, tree.heightScale, tree.widthScale)
            );

            if (!treeMeshes.ContainsKey(prototypeIndex))
            {
                treeMeshes[prototypeIndex] = new List<CombineInstance>();
                treePositions[prototypeIndex] = new List<Vector3>();
            }

            CombineInstance ci = new CombineInstance();
            ci.mesh = mesh;
            ci.transform = matrix;

            treeMeshes[prototypeIndex].Add(ci);
            treePositions[prototypeIndex].Add(worldPos);
        }

        int groupIndex = 0;
        foreach (var kvp in treeMeshes)
        {
            int prototypeIndex = kvp.Key;
            List<CombineInstance> combineList = kvp.Value;

            // Créer le mesh combiné
            Mesh combinedMesh = new Mesh();
            combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            combinedMesh.CombineMeshes(combineList.ToArray(), true, true);

            // Créer le GameObject du groupe
            GameObject lodGroupGO = new GameObject("TreeGroup_LOD_" + groupIndex);
            if (parentObject != null)
                lodGroupGO.transform.parent = parentObject.transform;

            // Position du LODGroup = centre moyen des positions
            Vector3 avgPosition = Vector3.zero;
            foreach (Vector3 pos in treePositions[prototypeIndex])
                avgPosition += pos;
            avgPosition /= treePositions[prototypeIndex].Count;
            lodGroupGO.transform.position = avgPosition;

            // LOD0 – Visible mesh
            GameObject lod0 = new GameObject("LOD0_Mesh");
            lod0.transform.parent = lodGroupGO.transform;
            lod0.transform.localPosition = Vector3.zero;

            MeshFilter mf = lod0.AddComponent<MeshFilter>();
            mf.mesh = combinedMesh;

            MeshRenderer mr = lod0.AddComponent<MeshRenderer>();
            mr.sharedMaterial = treePrototypes[prototypeIndex].prefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;

            // LOD1 – vide (disparaît à distance)
            GameObject lod1 = new GameObject("LOD1_Empty");
            lod1.transform.parent = lodGroupGO.transform;
            lod1.transform.localPosition = Vector3.zero;

            // LOD Group Setup
            LODGroup lodGroup = lodGroupGO.AddComponent<LODGroup>();
            LOD[] lods = new LOD[2];

            Renderer[] lod0Renderers = new Renderer[] { mr };
            Renderer[] lod1Renderers = new Renderer[] { }; // vide

            lods[0] = new LOD(lodTransitionHeight, lod0Renderers);
            lods[1] = new LOD(0.01f, lod1Renderers); // dernier niveau vide

            lodGroup.SetLODs(lods);
            lodGroup.RecalculateBounds();

            groupIndex++;
        }

        Debug.Log("Arbres combinés avec LODGroup en " + groupIndex + " groupes.");
    }
}
