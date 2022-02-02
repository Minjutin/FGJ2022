using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    //public bool playerDead = false;
    //public bool monsterDead = false;
    public bool gameHasEnded = false;

    public GameObject deathCanvas;
    EndCanvas end;

    private void Start()
    {
        end = FindObjectOfType<EndCanvas>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Milja_MainMenu");
        }

        //if (playerDead)
        //{
        //    deathCanvas.SetActive(true);
        //    StartCoroutine(WaitForMove());
        //}
    }

    // Ends the game in Player's Death
    public void EndGamePlayerDead()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            deathCanvas.SetActive(true);
            //string myString = "you died.";
            //end.ChangeEndingTextTo(myString);
            //end.ChangeEndingTextTo("TEST TEXT");
            end.PlayerDeadEnding();

            StartCoroutine(WaitForMove());
        }
        
    }

    // Ends the game in Monster's Death (aka Player WIN)
    public void EndGameMonsterDead()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            deathCanvas.SetActive(true);
            //string myString = "you escaped!";
            //end.ChangeEndingTextTo(myString);
            end.MonsterDeadEnding();

            StartCoroutine(WaitForMove());
        }
        
    }

    //return main menu
    IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Milja_MainMenu");
    }
}
