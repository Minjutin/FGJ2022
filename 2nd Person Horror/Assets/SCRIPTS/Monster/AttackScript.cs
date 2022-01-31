using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    GameObject gameManager;

    private void OnCollisionEnter(Collision other)
    {
            Debug.Log("Player is DEAD");

        if (other.gameObject.CompareTag("Player"))
        {
            gameManager = GameObject.Find("Game Manager");
            gameManager.GetComponent<GameOver>().playerDead = true;
        }
    }
}
