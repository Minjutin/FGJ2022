using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public bool playerDead = false;
    public GameObject deathCanvas;
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Milja_MainMenu");
        }
        if (playerDead)
        {
            deathCanvas.SetActive(true);
            StartCoroutine(WaitForMove());
        }


    }
        
    //return main menu
    IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Milja_MainMenu");
    }
}
