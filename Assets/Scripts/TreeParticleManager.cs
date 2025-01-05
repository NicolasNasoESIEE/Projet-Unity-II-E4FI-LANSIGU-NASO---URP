using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParticleManager : MonoBehaviour
{
    public Terrain terrain;
    public GameObject particlePrefab; 
    public float heightOffset = 2.0f; 
    public int targetTreePrototypeIndex = 0; 
    void Start()
    {
        if (terrain == null || particlePrefab == null)
        {
            Debug.LogError("Terrain ou particlePrefab n'est pas assigné !");
            return;
        }

        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            if (tree.prototypeIndex == targetTreePrototypeIndex)
            {
                Vector3 worldPosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

                worldPosition.y += heightOffset;

                Instantiate(particlePrefab, worldPosition, Quaternion.identity);
            }
        }
    }
}