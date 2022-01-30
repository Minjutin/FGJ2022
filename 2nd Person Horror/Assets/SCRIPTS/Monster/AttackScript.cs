using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    //[SerializeField] 
    GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Player is DEAD");

        if (other.gameObject.CompareTag("Player"))
        {
            gameManager = GameObject.Find("Game Manager");
            gameManager.GetComponent<GameOver>().playerDead = true;
        }
    }
}
