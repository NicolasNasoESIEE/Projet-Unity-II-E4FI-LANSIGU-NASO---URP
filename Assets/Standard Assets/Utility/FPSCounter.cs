using System;
using UnityEngine;
using UnityEngine.UI; // Add this for the UI namespace

namespace UnityStandardAssets.Utility
    {
    [RequireComponent(typeof(Text))] // Changed from GUIText to Text
    public class FPSCounter : MonoBehaviour
        {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "{0} FPS";
        private Text m_GuiText; // Changed from GUIText to Text


        private void Start()
            {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            m_GuiText = GetComponent<Text>(); // Changed from GUIText to Text
            }


        private void Update()
            {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
                {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_GuiText.text = string.Format(display, m_CurrentFps); // This remains the same
                }
            }
        }
    }
