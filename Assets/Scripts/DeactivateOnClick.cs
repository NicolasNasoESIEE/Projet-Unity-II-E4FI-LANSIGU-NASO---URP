using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnClick : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Aucun objet cible assigné dans le script DeactivateOnClick !");
            }
        }
    }
}
