using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RandomSpawnerAroundPlayer : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Terrain terrain;
    public float spawnOffset = 2f;
    public int numberToSpawn = 10;
    public XRInteractionManager interactionManager;

    void Start()
    {
        if (interactionManager == null)
        {
            Debug.LogWarning("XRInteractionManager n’est pas assigné !");
            return;
        }

        SpawnObjectsAroundPlayer();
    }

    void SpawnObjectsAroundPlayer()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;
        TreeInstance[] trees = terrainData.treeInstances;
        TreePrototype[] prototypes = terrainData.treePrototypes;

        // Trouver l'index du prototype "tree5"
        int tree5Index = -1;
        for (int i = 0; i < prototypes.Length; i++)
        {
            if (prototypes[i].prefab != null && prototypes[i].prefab.name == "tree5")
            {
                tree5Index = i;
                break;
            }
        }

        if (tree5Index == -1)
        {
            Debug.LogWarning("Aucun arbre avec le nom 'tree5' trouvé dans les prototypes.");
            return;
        }

        // Filtrer les arbres de type "tree5"
        TreeInstance[] tree5Instances = System.Array.FindAll(trees, t => t.prototypeIndex == tree5Index);
        int spawnCount = Mathf.Min(numberToSpawn, tree5Instances.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            TreeInstance tree = tree5Instances[Random.Range(0, tree5Instances.Length)];
            Vector3 worldTreePos = Vector3.Scale(tree.position, terrainData.size) + terrainPosition;

            Vector2 offset = Random.insideUnitCircle.normalized * spawnOffset;
            float x = worldTreePos.x + offset.x;
            float z = worldTreePos.z + offset.y;
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;

            Vector3 spawnPosition = new Vector3(x, y, z);
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);

            Instantiate(objectToSpawn, spawnPosition, rotation);
        }
    }
}
