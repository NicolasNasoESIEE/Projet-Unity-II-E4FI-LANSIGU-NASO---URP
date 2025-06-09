using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TPShip : MonoBehaviour
{
    [Header("XR Rig")]
    public Transform xrRigCamera;
    public float canvasDistance = 3f;

    [Header("UI Canvas")]
    public GameObject uiCanvas;

    [Header("XR Movement")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    private float toggleCooldown = 0.5f;
    private float lastToggleTime = 0f;
    private bool isUIVisible = false;

    void Start()
    {
        if (uiCanvas != null)
            uiCanvas.SetActive(false);
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isSecondaryPressed))
        {
            if (isSecondaryPressed && Time.time - lastToggleTime > toggleCooldown)
            {
                ToggleUI();
                lastToggleTime = Time.time;
            }
        }
    }

    void ToggleUI()
    {
        isUIVisible = !isUIVisible;

        if (uiCanvas == null)
            return;

        uiCanvas.SetActive(isUIVisible);

        if (isUIVisible)
        {
            PositionCanvasInFront();
            DisableMovement();
        }
        else
        {
            EnableMovement();
        }
    }

    void PositionCanvasInFront()
    {
        Vector3 forward = xrRigCamera.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 targetPosition = xrRigCamera.position + forward * canvasDistance;

        targetPosition.y = xrRigCamera.position.y - 1.1f;

        uiCanvas.transform.position = targetPosition;

        uiCanvas.transform.LookAt(new Vector3(xrRigCamera.position.x, targetPosition.y, xrRigCamera.position.z));
        uiCanvas.transform.Rotate(0, 180f, 0);
    }



    void DisableMovement()
    {
        if (moveProvider != null) moveProvider.enabled = false;
        if (turnProvider != null) turnProvider.enabled = false;
    }

    void EnableMovement()
    {
        if (moveProvider != null) moveProvider.enabled = true;
        if (turnProvider != null) turnProvider.enabled = true;
    }

    public void TriggerUIExternally()
    {
        if (!isUIVisible)
        {
            ToggleUI();
            lastToggleTime = Time.time;
        }
    }

}
