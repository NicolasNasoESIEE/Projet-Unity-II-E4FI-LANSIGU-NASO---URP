using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeParticleManager : MonoBehaviour
{
    public Terrain terrain; // Référence au terrain
    public GameObject particlePrefab; // Le prefab du système de particules
    public float heightOffset = 2.0f; // Offset vertical des particules

    void Start()
    {
        if (terrain == null || particlePrefab == null)
        {
            Debug.LogError("Terrain ou particlePrefab n'est pas assigné !");
            return;
        }

        // Récupérer les données des arbres du terrain
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            // Calculer la position du monde pour chaque arbre
            Vector3 worldPosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            // Ajouter un offset vertical
            worldPosition.y += heightOffset;

            // Instancier le système de particules à cette position
            Instantiate(particlePrefab, worldPosition, Quaternion.identity);
        }
    }
}
