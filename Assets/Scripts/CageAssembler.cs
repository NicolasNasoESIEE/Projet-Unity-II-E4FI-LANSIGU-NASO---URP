using System.Collections.Generic;
using UnityEngine;

public class CageAssembler : MonoBehaviour
{
    public GameObject completeCagePrefab;
    public Transform cageSpawnPoint;
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
        if (other.CompareTag("Stick") && sticksInZone.Contains(other.gameObject)) sticksInZone.Remove(other.gameObject);
    }

    private void CheckForCompletion()
    {
        if (sticksInZone.Count >= 3)
        {
            foreach (var stick in sticksInZone) Destroy(stick);
            sticksInZone.Clear();
            Instantiate(completeCagePrefab, cageSpawnPoint != null ? cageSpawnPoint.position : transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
