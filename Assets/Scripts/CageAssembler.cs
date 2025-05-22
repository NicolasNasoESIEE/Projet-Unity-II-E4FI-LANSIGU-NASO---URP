using System.Collections.Generic;
using UnityEngine;

public class CageAssembler : MonoBehaviour
{
    public GameObject completeCagePrefab;
    public Transform cageSpawnPoint;
    public AudioClip spawnSound;
    public ParticleSystem spawnParticlesPrefab;

    private List<GameObject> sticksInZone = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stick") && !sticksInZone.Contains(other.gameObject))
        {
            sticksInZone.Add(other.gameObject);
            CheckForCompletion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stick") && sticksInZone.Contains(other.gameObject))
            sticksInZone.Remove(other.gameObject);
    }

    private void CheckForCompletion()
    {
        if (sticksInZone.Count >= 3)
        {
            Vector3 spawnPosition = cageSpawnPoint != null ? cageSpawnPoint.position : transform.position;
            Quaternion spawnRotation = transform.rotation;

            foreach (var stick in sticksInZone)
                Destroy(stick);
            sticksInZone.Clear();

            Instantiate(completeCagePrefab, spawnPosition, spawnRotation);

            if (spawnSound != null)
                AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);

            if (spawnParticlesPrefab != null)
                Instantiate(spawnParticlesPrefab, spawnPosition, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
