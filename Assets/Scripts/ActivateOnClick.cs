using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnClick : MonoBehaviour
{
    public GameObject targetObject;

    private void OnMouseDown()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Aucun objet cible assigné dans le script ActivateOnClick !");
        }
    }
}