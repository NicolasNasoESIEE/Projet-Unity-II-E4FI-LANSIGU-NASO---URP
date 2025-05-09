using System;
using UnityEngine;
using UnityEngine.UI; // Add this for the UI namespace

namespace UnityStandardAssets.Utility
    {
    public class SimpleActivatorMenu : MonoBehaviour
        {
        // This menu controls the text displayed on a button
        // and the active state of a list of gameobjects
        public Text camSwitchButton; // Changed from GUIText to Text
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
            {
            // Active object starts from the first in the array
            m_CurrentActiveObject = 0;
            if (camSwitchButton != null && objects.Length > 0)
                {
                camSwitchButton.text = objects[m_CurrentActiveObject].name;
                }
            }

        public void NextCamera()
            {
            int nextactiveobject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
                {
                objects[i].SetActive(i == nextactiveobject);
                }

            m_CurrentActiveObject = nextactiveobject;
            if (camSwitchButton != null && objects.Length > 0)
                {
                camSwitchButton.text = objects[m_CurrentActiveObject].name;
                }
            }
        }
    }
