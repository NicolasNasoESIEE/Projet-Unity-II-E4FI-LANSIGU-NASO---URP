using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnProximityAndKeyPress : MonoBehaviour
{
    public GameObject player; // Référence au joueur
    public GameObject objectToActivate; // L'objet à activer
    public GameObject objectToDeactivate; // L'objet à activer
    public float activationDistance = 10f; // Distance à laquelle le joueur doit être
    private bool isPlayerInRange = false; // Pour vérifier si le joueur est à portée

    void Update()
    {
        // Vérifier la distance entre le joueur et l'objet
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Si le joueur est dans la portée définie
        isPlayerInRange = distance <= activationDistance;

        // Si le joueur est proche et appuie sur K
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.K) && GlobalState.valide==2)
        {
            // Activer l'objet
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
            Debug.Log("Objet activé !");
        }
    }
}