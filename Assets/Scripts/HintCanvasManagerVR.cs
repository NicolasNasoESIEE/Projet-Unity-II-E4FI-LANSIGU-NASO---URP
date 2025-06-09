using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HintCanvasManagerVR : MonoBehaviour
{
    [Header("XR Rig")]
    public Transform xrRigCamera;
    public float canvasDistance = 5f;

    [Header("Canvas Parent")]
    public Transform canvasParent;

    [Header("XR Movement")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    private int hintCount = 0;
    private int maxHints = 3;
    private string currentTab = "map";

    private float toggleCooldown = 0.5f;
    private float lastToggleTime = 0f;
    private bool anyCanvasActive = false;

    // Boutons actifs pour le canvas courant
    private Button mapButton;
    private Button hintButton;
    private Button solButton;

    void Start()
    {
        HideAllCanvases();
        UpdateUI(); // Optionnel pour afficher dès le départ
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Toggle UI
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPrimaryPressed))
        {
            if (isPrimaryPressed && Time.time - lastToggleTime > toggleCooldown)
            {
                ToggleUI();
                lastToggleTime = Time.time;
            }
        }

    }

    void ToggleUI()
    {
        if (anyCanvasActive)
        {
            HideAllCanvases();
            EnableMovement();
        }
        else
        {
            UpdateUI();
            DisableMovement();
        }
    }

    void AddHint()
    {
        if (hintCount < maxHints)
        {
            hintCount++;
            UpdateUI();
        }
    }

    public void AddHintExternally()
    {
        AddHint();
    }

    void SwitchTab(string tabName)
    {
        currentTab = tabName;
        UpdateUI();
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

    void UpdateUI()
    {
        string canvasName = GetCanvasNameFromState();
        Transform newCanvas = canvasParent.Find(canvasName);

        if (newCanvas == null)
        {
            Debug.LogWarning("Canvas not found: " + canvasName);
            anyCanvasActive = false;
            return;
        }

        // Stocker position/rotation du canvas actif
        GameObject currentCanvas = GetActiveCanvas();

        HideAllCanvases();
        newCanvas.gameObject.SetActive(true);
        anyCanvasActive = true;

        if (currentCanvas != null && currentCanvas != newCanvas.gameObject)
        {
            newCanvas.position = currentCanvas.transform.position;
            newCanvas.rotation = currentCanvas.transform.rotation;
        }
        else if (currentCanvas == null)
        {
            PositionCanvasInFront(newCanvas.gameObject);
        }

        BindButtonsFromCanvas(newCanvas);
    }



    void BindButtonsFromCanvas(Transform canvas)
    {
        // Clear previous listeners
        if (mapButton != null) mapButton.onClick.RemoveAllListeners();
        if (hintButton != null) hintButton.onClick.RemoveAllListeners();
        if (solButton != null) solButton.onClick.RemoveAllListeners();

        // Find buttons in this canvas
        mapButton = canvas.Find("Button")?.GetComponent<Button>();
        hintButton = canvas.Find("Button (1)")?.GetComponent<Button>();
        solButton = canvas.Find("Button (2)")?.GetComponent<Button>();

        if (mapButton != null) mapButton.onClick.AddListener(() => SwitchTab("map"));
        if (hintButton != null) hintButton.onClick.AddListener(() => SwitchTab("hint"));
        if (solButton != null) solButton.onClick.AddListener(() => SwitchTab("sol"));
    }

    string GetCanvasNameFromState()
    {
        if (currentTab == "sol")
        {
            return hintCount >= 3 ? "3_sol" : "0_sol";
        }

        int index = Mathf.Clamp(hintCount, 0, 3);
        return $"{index}_{currentTab}";
    }

    void HideAllCanvases()
    {
        foreach (Transform child in canvasParent)
        {
            child.gameObject.SetActive(false);
        }
        anyCanvasActive = false;
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
