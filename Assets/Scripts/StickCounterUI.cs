using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Utilise UnityEngine.UI si tu n’utilises pas TMP

public class StickCounterUI : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    void Update()
    {
        counterText.text = " " + StickCounter.collectedSticks;
    }
}