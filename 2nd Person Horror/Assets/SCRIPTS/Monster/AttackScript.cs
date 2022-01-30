using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit something!");

        if (collision.gameObject.CompareTag("Player"))
        {
            //KILL PLAYER
            Debug.Log("Player is DEAD");
        }
    }
}
