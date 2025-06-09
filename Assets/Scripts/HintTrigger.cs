using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public HintCanvasManagerVR hintManager;
    public GameObject objectToActivate; // Le GameObject à activer après contact
    public bool destroyAfterTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintManager != null)
            {
                hintManager.AddHintExternally();
            }

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            if (destroyAfterTrigger)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false); // désactiver si tu préfères
            }
        }
    }
}
