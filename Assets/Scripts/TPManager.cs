using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TPManager : MonoBehaviour
{
    public string sceneName;
    public Button switchToCanvas2Button;
    
     void Start()
    {
        if (switchToCanvas2Button != null)
            switchToCanvas2Button.onClick.AddListener(SwitchToScene);
    }

    void SwitchToScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
