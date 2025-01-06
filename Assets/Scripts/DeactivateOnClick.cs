using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnClick : MonoBehaviour
{
    public GameObject targetObject;

    public void ActivateTarget(){
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Aucun objet cible assigné dans le script ActivateOnClick !");
        }
    }
}