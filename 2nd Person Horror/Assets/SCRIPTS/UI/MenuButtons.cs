using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject levelCanvas;

    public void StartGame()
    {
        levelCanvas.SetActive(true); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
