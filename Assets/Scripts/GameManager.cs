using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameObject principal")]
    public GameObject mainObject;
    public GameObject teleport;
    

    [Header("GameObjects à activer/désactiver")]
    public GameObject Menu;
    public GameObject Eau; // Activé avec &
    public GameObject EauSuccess;
    public GameObject Feu; // Activé avec é
    public GameObject FeuSuccess;
    public GameObject Autre; // Activé avec "
    public GameObject AutreSuccess;

    public GameObject[] PanelsList;

    private void Start()
    {
        PanelsList = new GameObject[] { Menu, Eau, EauSuccess, Feu, FeuSuccess, Autre, AutreSuccess };
    }

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateDeactivate(EauSuccess, Eau);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleMainObject();
        }

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
                ActivateDeactivate(Eau, Menu);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Touche é
            {
                ActivateDeactivate(Feu, Menu);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // Touche "
            {
                ActivateDeactivate(Autre, Menu);
            }
        }
    }


    void SetActivated(GameObject toActivate){
        toActivate.SetActive(true);
    }
    void Deactivated(GameObject toActivate){
        toActivate.SetActive(false);
    }
    void Toggle(GameObject toActivate){
        toActivate.SetActive(!toActivate.activeSelf);
    }

    void ToggleMainObject()
    {

        foreach (GameObject panel in PanelsList)
        {
            ActivateDeactivate(Menu, panel);
        }

        if (mainObject != null)
        {
            mainObject.SetActive(!mainObject.activeSelf);
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