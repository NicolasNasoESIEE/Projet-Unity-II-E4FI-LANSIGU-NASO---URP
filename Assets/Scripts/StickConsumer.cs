using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickConsumer : MonoBehaviour
{
    public GameObject objectToSpawn; // Le prefab à créer après le clic

    private void OnMouseDown()
    {
        if (StickCounter.TrySpendSticks(3))
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            Destroy(gameObject); // Optionnel : supprimer l'objet après interaction
        }
        else
        {
            Debug.Log("Pas assez de bâtons !");
        }
    }
}