using UnityEngine;
using UnityEngine.UI;

public class StartMenuManagerVR : MonoBehaviour
{
    public GameObject vrCanvas;
    public Button startButton;

    void Start()
    {
        Time.timeScale = 0f;
        vrCanvas.SetActive(true);
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        Time.timeScale = 1f;
        vrCanvas.SetActive(false);
    }
}
