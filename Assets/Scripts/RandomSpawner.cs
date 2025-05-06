using UnityEngine;

public class RandomSpawnerAroundPlayer : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Terrain terrain;
    public float spawnOffset = 2f;  // Distance par rapport à l’arbre
    public int numberToSpawn = 10;

    void Start()
    {
        SpawnObjectsAroundPlayer();
    }

    void SpawnObjectsAroundPlayer()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;
        TreeInstance[] trees = terrainData.treeInstances;

        int spawnCount = Mathf.Min(numberToSpawn, trees.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            TreeInstance tree = trees[Random.Range(0, trees.Length)];

            // Convertir la position normalisée de l’arbre en position monde
            Vector3 worldTreePos = Vector3.Scale(tree.position, terrainData.size) + terrainPosition;

            // Générer une position légèrement décalée autour de l’arbre
            Vector2 offset = Random.insideUnitCircle.normalized * spawnOffset;
            float x = worldTreePos.x + offset.x;
            float z = worldTreePos.z + offset.y;
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;

            Vector3 spawnPosition = new Vector3(x, y, z);
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);  // Rotation X de 90°

            Instantiate(objectToSpawn, spawnPosition, rotation);
        }
    }
}
