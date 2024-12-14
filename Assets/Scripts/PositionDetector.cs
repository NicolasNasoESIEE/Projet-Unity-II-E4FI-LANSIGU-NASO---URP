using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDetector : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 margin = new Vector3(2f, 4f, 2f);
    [SerializeField] private AudioClip arrivalSound;
    [SerializeField] private AudioSource audioSource;

    private bool hasPlayed = false;

    private void Update()
    {
        if (!hasPlayed && IsWithinMargin(transform.position, targetPosition, margin))
        {
            PlayArrivalSound();
        }
    }

    private bool IsWithinMargin(Vector3 position, Vector3 target, Vector3 margin)
    {
        return Mathf.Abs(position.x - target.x) <= margin.x &&
               Mathf.Abs(position.y - target.y) <= margin.y &&
               Mathf.Abs(position.z - target.z) <= margin.z;
    }

    private void PlayArrivalSound()
    {
        if (audioSource != null && arrivalSound != null)
        {
            audioSource.PlayOneShot(arrivalSound);
            hasPlayed = true;
        }
    }
}


