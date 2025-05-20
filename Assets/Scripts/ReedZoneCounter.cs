using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ReedZoneCounter : MonoBehaviour
{
    public GameObject reedPrefab;
    public Transform spawnParent;
    public LayerMask placementLayer;
    public Transform countZone;
    public float countRadius = 5f;
    public string reedTag = "Reed";
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI totalLimitText; // facultatif : texte pour la limite
    public Camera mainCamera;

    private const int maxReeds = 20;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySpawnReed();
        }

        UpdateReedCountDisplay();
    }

    void TrySpawnReed()
    {
        int currentTotal = GameObject.FindGameObjectsWithTag(reedTag).Length;

        if (currentTotal >= maxReeds)
        {
            if (totalLimitText != null)
                totalLimitText.text = "/ 20 roseaux";
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            GameObject newReed = Instantiate(reedPrefab, hit.point, Quaternion.identity, spawnParent);
            newReed.tag = reedTag;
        }
    }

    void UpdateReedCountDisplay()
    {
        if (countZone == null || counterText == null) return;

        int count = 0;
        Collider[] nearby = Physics.OverlapSphere(countZone.position, countRadius);
        foreach (Collider col in nearby)
        {
            if (col.CompareTag(reedTag))
                count++;
        }

        counterText.text = "Reeds nearby: " + count;
    }

    void OnDrawGizmosSelected()
    {
        if (countZone != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(countZone.position, countRadius);
        }
    }
}