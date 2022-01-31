using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_script : MonoBehaviour
{
    public float movementSpeed = 30f;
    GameObject gameManager;

    void Update ()
    {
        gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Monster"))
        {
            gameManager = GameObject.Find("Game Manager");
            gameManager.GetComponent<GameOver>().playerDead = true;
        }
    }

}
