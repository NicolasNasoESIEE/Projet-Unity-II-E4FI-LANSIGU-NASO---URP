using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPickUp : MonoBehaviour
{
    void OnMouseDown()
    {
        StickCounter.AddStick();
        Destroy(gameObject);
    }
}
