using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class How_ezy : MonoBehaviour
{
    // Start is called before the first frame update
    public void easy()
    {
        PlayerPrefs.SetInt("difficulty", 0);
        SceneManager.LoadScene("Milja2");
    }

    // Update is called once per frame
    public void normal()
    {
        PlayerPrefs.SetInt("difficulty", 1);
        SceneManager.LoadScene("Milja2");
    }
}
