using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneChanger : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        if (targetObject != null && targetObject.activeInHierarchy && Input.GetKeyDown(KeyCode.B))
        {
            ChangeToScene2();
        }
    }

    void ChangeToScene2()
    {
        SceneManager.LoadScene(2); 
    }
}