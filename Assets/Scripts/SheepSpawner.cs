using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [Header("Mouton Settings")]
    public GameObject sheepPrefab; // Le prefab du mouton à instancier

    void Update()
    {
        // Si la touche M est pressée, on instancie un mouton
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnSheep();
        }
    }

    void SpawnSheep()
    {

        // Instancier le mouton à la position calculée
        Instantiate(sheepPrefab, transform.position, Quaternion.identity);
    }
}
