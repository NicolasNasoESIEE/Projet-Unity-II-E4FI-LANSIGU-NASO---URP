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
        else
            Debug.LogWarning("Aucun bouton UI assigné !");
    }

    void SwitchToScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la scène n'est pas défini !");
        }
    }
}
