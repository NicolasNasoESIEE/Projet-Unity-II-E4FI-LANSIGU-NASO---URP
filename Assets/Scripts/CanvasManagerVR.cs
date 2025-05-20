using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasManagerVR : MonoBehaviour
{
    [Header("Canvas & Interaction")]
    public Transform xrRigCamera;
    public float canvasDistance = 5f;
    public Transform canvasParent;
    public GameObject canvas1;
    public GameObject canvas2;

    [Header("Boutons")]
    public Button switchToCanvas2Button;
    public Button switchToCanvas1Button;

    [Header("XR Movement")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    private float toggleCooldown = 0.5f;
    private float lastToggleTime = 0f;
    private bool anyCanvasActive = false;

    void Start()
    {
        HideAllCanvases();

        if (switchToCanvas2Button != null)
            switchToCanvas2Button.onClick.AddListener(SwitchToCanvas2);

        if (switchToCanvas1Button != null)
            switchToCanvas1Button.onClick.AddListener(SwitchToCanvas1);
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            if (isPressed && Time.time - lastToggleTime > toggleCooldown)
            {
                HandlePrimaryButtonToggle();
                lastToggleTime = Time.time;
            }
        }
    }

    void HandlePrimaryButtonToggle()
    {
        if (anyCanvasActive)
        {
            HideAllCanvases();
            EnableMovement();
        }
        else
        {
            ShowCanvas(canvas1);
            DisableMovement();
        }
    }

    GameObject GetActiveCanvas()
    {
        foreach (Transform child in canvasParent)
        {
            if (child.gameObject.activeSelf)
                return child.gameObject;
        }
        return null;
    }


    void ShowCanvas(GameObject newCanvas)
    {
        GameObject currentActiveCanvas = GetActiveCanvas();

        HideAllCanvases();

        if (newCanvas != null)
        {
            newCanvas.SetActive(true);

            if (currentActiveCanvas != null)
            {
                newCanvas.transform.position = currentActiveCanvas.transform.position;
                newCanvas.transform.rotation = currentActiveCanvas.transform.rotation;
            }
            else
            {
                PositionCanvasInFront(newCanvas);
            }

            anyCanvasActive = true;
            DisableMovement();
        }
    }

    void HideAllCanvases()
    {
        foreach (Transform child in canvasParent)
        {
            child.gameObject.SetActive(false);
        }
        anyCanvasActive = false;
    }

    void SwitchToCanvas2()
    {
        ShowCanvas(canvas2);
    }

    void SwitchToCanvas1()
    {
        ShowCanvas(canvas1);
    }

    void PositionCanvasInFront(GameObject canvas)
    {
        Vector3 forward = xrRigCamera.forward;
        forward.y = 0;
        forward.Normalize();

        canvas.transform.position = xrRigCamera.position + forward * canvasDistance;
        canvas.transform.LookAt(xrRigCamera.position);
        canvas.transform.Rotate(0, 180, 0);
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
}
