using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    GameObject gameManager;
    MonsterPathfinding monsterAI;

    public bool attackCollision = false;

    private void Start()
    {
        monsterAI = FindObjectOfType<MonsterPathfinding>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!monsterAI.GetHuntingMode())
            {
                // Player bumbs into Monster --> aggro monster
                monsterAI.EnterHuntingMode(3f);
            }
            else if (attackCollision) // Player is caught by aggroed Monster --> DIE
            {
                Debug.Log("Player is DEAD");
                //gameManager = GameObject.Find("Game Manager");
                //gameManager.GetComponent<GameOver>().playerDead = true;

                FindObjectOfType<GameOver>().EndGamePlayerDead();
            }
        }
    }
}
