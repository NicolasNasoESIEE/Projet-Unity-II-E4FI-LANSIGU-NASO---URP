using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public AudioClip specialAudio;
    private bool hasPlayed = false;

    void Start()
    {
        if (CageManager.Instance != null && CageManager.Instance.allCagesAssembled && !hasPlayed)
        {
            if (specialAudio != null)
            {
                AudioSource.PlayClipAtPoint(specialAudio, Camera.main.transform.position);
                hasPlayed = true;
            }
        }
    }
}
