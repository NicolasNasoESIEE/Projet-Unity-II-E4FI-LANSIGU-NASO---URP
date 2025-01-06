using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpActivate2 : MonoBehaviour
{
    
    public GameObject Uitoactivate;
    // Update is called once per frame
    void Update()
    {
        if(GlobalState.valide==1){
            Uitoactivate.SetActive(true);
            GlobalState.valide = 2;
        }
    }
}
