using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public AudioClip endAudio;
    public GameObject vrCanvasEnd;
    private bool hasPlayed = false;

    void Start()
    {
        if (CageManager.Instance != null && CageManager.Instance.allCagesAssembled && !hasPlayed)
        {
            if (endAudio != null)
            {
                AudioSource.PlayClipAtPoint(endAudio, Camera.main.transform.position);
                hasPlayed = true;
            }
        }
    }

    void Update()
    {
        if(hasPlayed)
        {
            if (CageManager.Instance != null && CageManager.Instance.allCagesAssembled)
            {
                vrCanvasEnd.SetActive(true);
            }
            else
            {
                vrCanvasEnd.SetActive(false);
            }
        }
    }
}
