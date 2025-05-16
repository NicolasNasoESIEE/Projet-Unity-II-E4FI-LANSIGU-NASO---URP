using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;  

public class ReedZoneCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText; // ou TMP_Text si tu utilises TextMeshPro
    private HashSet<GameObject> reedsInZone = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roseau"))
        {
            reedsInZone.Add(other.gameObject);
            UpdateCounter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Roseau"))
        {
            reedsInZone.Remove(other.gameObject);
            UpdateCounter();
        }
    }

    void UpdateCounter()
{
    if (counterText != null)
    {
        counterText.text = "Roseaux dans la zone : " + reedsInZone.Count;
        Debug.Log("Compteur mis à jour : " + reedsInZone.Count);
    }
    else
    {
        Debug.LogWarning("Le champ 'counterText' n'est pas assigné !");
    }
}
}