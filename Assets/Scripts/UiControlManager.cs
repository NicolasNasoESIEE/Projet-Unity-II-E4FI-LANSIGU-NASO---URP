using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; // Assure-toi d'avoir ce namespace si tu utilises le FPSController standard

public class UIControlManager : MonoBehaviour
{
    public GameObject uiPanel; // L'UI à surveiller
    public FirstPersonController fpsController; // Assigne le FirstPersonController dans l'inspecteur

    void Update()
    {
        if (uiPanel != null && fpsController != null)
        {
            if (uiPanel.activeSelf)
            {
                fpsController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                fpsController.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
