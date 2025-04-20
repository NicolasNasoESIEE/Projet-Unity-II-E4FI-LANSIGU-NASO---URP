using System.Collections.Generic;
using UnityEngine;

public class TreeParticleManager : MonoBehaviour
{
    public Terrain terrain;
    public GameObject particlePrefab;
    public float heightOffset = 2.0f;
    public int targetTreePrototypeIndex = 0;
    public float groupRadius = 10f; // Rayon pour regrouper les arbres

    private List<Vector3> particlePositions = new List<Vector3>();

    void Start()
    {
        if (terrain == null || particlePrefab == null)
        {
            Debug.LogError("Terrain ou particlePrefab n'est pas assigné !");
            return;
        }

        TreeInstance[] trees = terrain.terrainData.treeInstances;

        // Récupère les positions des arbres ciblés
        foreach (TreeInstance tree in trees)
        {
            if (tree.prototypeIndex == targetTreePrototypeIndex)
            {
                Vector3 worldPosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;
                worldPosition.y += heightOffset;
                particlePositions.Add(worldPosition);
            }
        }

        // Grouper les positions proches
        List<Vector3> groupedPositions = GroupPositions(particlePositions, groupRadius);

        // Instancier un système de particules par groupe
        foreach (Vector3 pos in groupedPositions)
        {
            GameObject particle = Instantiate(particlePrefab, pos, Quaternion.identity);

            // Optimisation : réduire les particules générées
            var ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.rateOverTime = emission.rateOverTime.constant * 0.2f; // Réduit à 20% de l’émission normale
            }
        }
    }

    // Méthode de regroupement simple par distance
    List<Vector3> GroupPositions(List<Vector3> positions, float radius)
    {
        List<Vector3> groups = new List<Vector3>();
        HashSet<int> used = new HashSet<int>();

        for (int i = 0; i < positions.Count; i++)
        {
            if (used.Contains(i)) continue;

            Vector3 center = positions[i];
            List<Vector3> group = new List<Vector3> { center };
            used.Add(i);

            for (int j = i + 1; j < positions.Count; j++)
            {
                if (!used.Contains(j) && Vector3.Distance(center, positions[j]) <= radius)
                {
                    group.Add(positions[j]);
                    used.Add(j);
                }
            }

            // Calculer le centre moyen du groupe
            Vector3 average = Vector3.zero;
            foreach (var p in group)
                average += p;
            average /= group.Count;

            groups.Add(average);
        }

        return groups;
    }
}
