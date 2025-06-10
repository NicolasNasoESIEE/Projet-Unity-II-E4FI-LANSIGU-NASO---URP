using UnityEngine;
using UnityEngine.UI;

public class StartMenuManagerVR : MonoBehaviour
{
    public GameObject vrCanvas;
    public Button startButton;

    private static bool hasStartedOnce = false;

    void Start()
    {
        if (hasStartedOnce)
        {
            vrCanvas.SetActive(false);
            Time.timeScale = 1f;
            return;
        }

        Time.timeScale = 0f;
        vrCanvas.SetActive(true);
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        Time.timeScale = 1f;
        vrCanvas.SetActive(false);
        hasStartedOnce = true;
    }
}
