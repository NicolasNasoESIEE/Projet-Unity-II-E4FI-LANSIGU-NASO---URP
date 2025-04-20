using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExport : MonoBehaviour
{
    public Terrain terrain;
    public int targetTreePrototypeIndex = -1; // -1 = tous les types d'arbres

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain non assigné.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] trees = terrainData.treeInstances;
        TreePrototype[] prototypes = terrainData.treePrototypes;

        if (prototypes == null || prototypes.Length == 0)
        {
            Debug.LogError("Aucun prototype d'arbre trouvé.");
            return;
        }

        List<CombineInstance> combineInstances = new List<CombineInstance>();
        Material sharedMaterial = null;

        foreach (TreeInstance tree in trees)
        {
            int prototypeIndex = tree.prototypeIndex;

            if (targetTreePrototypeIndex != -1 && prototypeIndex != targetTreePrototypeIndex)
                continue;

            GameObject prototypeGO = prototypes[prototypeIndex].prefab;
            MeshFilter mf = prototypeGO.GetComponentInChildren<MeshFilter>();
            MeshRenderer mr = prototypeGO.GetComponentInChildren<MeshRenderer>();

            if (mf == null || mr == null)
            {
                Debug.LogWarning("Arbre ignoré : pas de MeshFilter ou MeshRenderer.");
                continue;
            }

            if (sharedMaterial == null)
                sharedMaterial = mr.sharedMaterial;

            // Position en monde réel
            Vector3 worldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;
            Quaternion rotation = Quaternion.Euler(0, tree.rotation * 360f, 0);
            Vector3 scale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);

            Matrix4x4 matrix = Matrix4x4.TRS(worldPos, rotation, scale);

            combineInstances.Add(new CombineInstance
            {
                mesh = mf.sharedMesh,
                transform = matrix
            });
        }

        // Créer le GameObject combiné
        GameObject combinedGO = new GameObject("CombinedTrees");
        combinedGO.transform.position = Vector3.zero;

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

        MeshFilter combinedFilter = combinedGO.AddComponent<MeshFilter>();
        combinedFilter.mesh = combinedMesh;

        MeshRenderer combinedRenderer = combinedGO.AddComponent<MeshRenderer>();
        combinedRenderer.sharedMaterial = sharedMaterial;

        Debug.Log("✅ Arbres combinés : " + combineInstances.Count);

    }
}
