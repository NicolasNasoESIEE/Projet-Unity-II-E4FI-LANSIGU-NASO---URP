using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleMap : MonoBehaviour
{
    public GameObject canvasVR;
    public Transform xrRigCamera;
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;
    public float canvasDistance = 5f;
    private float toggleCooldown = 0.5f;
    private float lastToggleTime = 0f;

    private bool canvasActive = false;

    void Start()
    {
        canvasVR.SetActive(false);
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            if (isPressed && Time.time - lastToggleTime > toggleCooldown)
            {
                ToggleCanvas();
                lastToggleTime = Time.time;
            }
        }

        /*if (canvasActive) // pour rotation horizontale avec le casque
        {
            Vector3 forward = xrRigCamera.forward;
            forward.y = 0;
            forward.Normalize();

            canvasVR.transform.position = xrRigCamera.position + forward * canvasDistance;
            canvasVR.transform.LookAt(xrRigCamera.position);
            canvasVR.transform.Rotate(0, 180, 0);
        }*/
    }

    void ToggleCanvas()
    {
        canvasActive = !canvasActive;

        canvasVR.SetActive(canvasActive);

        if (canvasActive)
        {
            Vector3 forward = xrRigCamera.forward;
            forward.y = 0;
            forward.Normalize();

            canvasVR.transform.position = xrRigCamera.position + forward * canvasDistance;
            canvasVR.transform.LookAt(xrRigCamera.position);
            canvasVR.transform.Rotate(0, 180, 0);
        }

        if (moveProvider != null) moveProvider.enabled = !canvasActive;
        if (turnProvider != null) turnProvider.enabled = !canvasActive;
    }
}
