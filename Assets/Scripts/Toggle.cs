using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [Header("GameObject principal")]
    public GameObject mainObject;
    public GameObject teleport;

    [Header("GameObjects à activer/désactiver")]
    public GameObject object1; // Activé avec &
    public GameObject object2; // Activé avec é
    public GameObject object3; // Activé avec "
    public GameObject object4; // Désactivé avec &

    void Update()
    {
        // Toggle principal avec T
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleMainObject();
        }

        // Toggle principal avec T
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleTeleport();
        }

        // Vérifie si le GameObject principal est actif
        if (mainObject != null && mainObject.activeSelf)
        {
            // Activation/Désactivation des autres objets
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Touche &
            {
                ActivateDeactivate(object1, object4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Touche é
            {
                ActivateDeactivate(object2, object4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // Touche "
            {
                ActivateDeactivate(object3, object4);
            }
        }
    }

    void ToggleMainObject()
    {
        if (mainObject != null)
        {
            mainObject.SetActive(!mainObject.activeSelf);
            ActivateDeactivate(object4, object1);
        }
        else
        {
            Debug.LogWarning("Aucun GameObject principal assigné au script !");
        }
    }
    void ToggleTeleport()
    {
        if (teleport != null)
        {
            teleport.SetActive(!teleport.activeSelf);
        }
        else
        {
            Debug.LogWarning("Aucun GameObject principal assigné au script !");
        }
    }

    void ActivateDeactivate(GameObject toActivate, GameObject toDeactivate)
    {
        if (toActivate != null)
        {
            toActivate.SetActive(true);
        }

        if (toDeactivate != null)
        {
            toDeactivate.SetActive(false);
        }
    }
}