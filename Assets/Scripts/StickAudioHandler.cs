using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource), typeof(XRGrabInteractable))]
public class StickAudioHandler : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip impactSound;

    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;
    private bool hasPlayedImpact = false;
    private bool hasBeenGrabbed = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        hasPlayedImpact = false;
        hasBeenGrabbed = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenGrabbed) return;

        if (impactSound != null && !hasPlayedImpact)
        {
            audioSource.PlayOneShot(impactSound);
            hasPlayedImpact = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hasPlayedImpact = false;
    }
}
