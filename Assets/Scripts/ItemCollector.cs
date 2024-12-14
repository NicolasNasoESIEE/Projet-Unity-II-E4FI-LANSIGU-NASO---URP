using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int itemCount = 0;

    [SerializeField] TextMeshProUGUI itemCounterText;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        itemCounterText.text = "Items: ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MachinePiece"))
        {
            itemCount++;
            UpdateItemCounter();
            PlayPickupSound();
            Destroy(other.gameObject);
        }
    }

    private void UpdateItemCounter()
    {
        itemCounterText.text = "Items: " + itemCount;
    }

    private void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
