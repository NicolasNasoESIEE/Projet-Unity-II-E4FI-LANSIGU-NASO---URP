using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Méthode pour changer de scène
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }


    public void ChangeScene1()
    {
        SceneManager.LoadScene(1);
    }
}
