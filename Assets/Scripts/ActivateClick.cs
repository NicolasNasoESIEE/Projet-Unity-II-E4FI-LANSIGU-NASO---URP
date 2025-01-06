using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateClick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;

    public void ActivateTarget(){
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
