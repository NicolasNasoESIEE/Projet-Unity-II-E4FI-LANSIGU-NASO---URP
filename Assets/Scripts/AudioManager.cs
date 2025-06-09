using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject canvasA;
    public GameObject canvasB;

    [Header("Buttons")]
    public Button toCanvasBButton; // Sur Canvas A
    public Button toCanvasAButton; // Sur Canvas B


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        // Initial state: show canvasA only
        canvasA.SetActive(true);
        canvasB.SetActive(false);

        // Assign listeners
        if (toCanvasBButton != null)
            toCanvasBButton.onClick.AddListener(SwitchToCanvasB);
        if (toCanvasAButton != null)
            toCanvasAButton.onClick.AddListener(SwitchToCanvasA);
    }

    void SwitchToCanvasB()
    {
        canvasA.SetActive(false);
        canvasB.SetActive(true);

        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    void SwitchToCanvasA()
    {
        canvasA.SetActive(true);
        canvasB.SetActive(false);

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}