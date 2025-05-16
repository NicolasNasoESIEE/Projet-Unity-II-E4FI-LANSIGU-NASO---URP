using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class VRReedsSpawner : MonoBehaviour
{
    public GameObject reedPrefab;
    public Transform spawnParent;
    public LayerMask placementLayer;
    public float rayLength = 10f;

    public Transform leftHandControllerTransform; // pour viser
    public TextMeshProUGUI counterText;
    private InputDevice leftHandDevice;
    private int reedCount = 0;
    private bool wasButtonPressedLastFrame = false;

    void Start()
    {
        // Obtenir la manette gauche
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        // Si le device n'est plus valide, on le recherche
        if (!leftHandDevice.isValid)
        {
            leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        // Lire l’état du bouton X (primaryButton sur main gauche)
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            // Détection du front montant (pression instantanée)
            if (isPressed && !wasButtonPressedLastFrame)
            {
                TrySpawnReed();
            }

            wasButtonPressedLastFrame = isPressed;
        }
    }

    void TrySpawnReed()
    {
        if (leftHandControllerTransform == null) return;

        Ray ray = new Ray(leftHandControllerTransform.position, leftHandControllerTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, placementLayer))
        {
            Instantiate(reedPrefab, hit.point, Quaternion.identity, spawnParent);
            reedCount++;

            if (counterText)
                counterText.text = "Reeds: " + reedCount;
        }
    }
}
