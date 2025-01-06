using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Uitoactivate;

    // Update is called once per frame
    void Update()
    {
        if(GlobalState.valide==1){
            Uitoactivate.SetActive(true);
        }
    }
}
