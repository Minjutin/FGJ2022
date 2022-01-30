using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    //[SerializeField] 
    GameObject gameManager;

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Player is DEAD");

        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager = GameObject.Find("Game Manager");
            gameManager.GetComponent<GameOver>().playerDead = true;
        }
    }
}
