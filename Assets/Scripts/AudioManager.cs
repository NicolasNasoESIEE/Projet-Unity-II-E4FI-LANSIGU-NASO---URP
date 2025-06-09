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

    private bool isWaitingForAudioEnd = false;

    void Start()
    {
        canvasA.SetActive(true);
        canvasB.SetActive(false);

        if (toCanvasBButton != null)
            toCanvasBButton.onClick.AddListener(SwitchToCanvasB);
        if (toCanvasAButton != null)
            toCanvasAButton.onClick.AddListener(SwitchToCanvasA);
    }

    void Update()
    {
        // Si on attend la fin de l’audio, et que le son est terminé
        if (isWaitingForAudioEnd && !audioSource.isPlaying)
        {
            isWaitingForAudioEnd = false;
            SwitchToCanvasA(); // Revenir à canvas A automatiquement
        }
    }

    void SwitchToCanvasB()
    {
        canvasA.SetActive(false);
        canvasB.SetActive(true);

        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            isWaitingForAudioEnd = true;
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

        isWaitingForAudioEnd = false;
    }
}