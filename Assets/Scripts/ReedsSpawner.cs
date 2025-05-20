using UnityEngine;
using TMPro;  // si tu veux afficher le compteur

public class ReedsSpawner : MonoBehaviour
{
    public GameObject reedPrefab; // assigner la prefab dans l'inspecteur
    public Transform spawnParent; // optionnel, pour organiser dans la hiérarchie
    public TextMeshProUGUI counterText; // si tu veux afficher le nombre à l'écran
    private int reedCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clic gauche
        {
            SpawnReed();
        }
    }

    void SpawnReed()
    {
        Vector3 spawnPosition = GetMouseWorldPosition();
        if (spawnPosition != Vector3.zero)
        {
            Instantiate(reedPrefab, spawnPosition, Quaternion.identity, spawnParent);
            reedCount++;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

   
}
