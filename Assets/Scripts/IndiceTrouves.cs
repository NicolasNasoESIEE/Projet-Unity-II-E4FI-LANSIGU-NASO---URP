using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndiceTrouves : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    private void OnMouseDown()
    {
        if (targetObject != null)
        {
            GlobalState.valide = 1;
            targetObject.SetActive(true);
        }
    }
}
