using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public bool canBeUsed = false;

    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {

        //If player decides to shoot.
        if (canBeUsed && Input.GetMouseButtonDown(0))
        {
            //PUT LOUD SOUND HERE
            Instantiate(projectile, transform.position, Quaternion.identity);
            canBeUsed = false;

        }

        if(!canBeUsed && Input.GetMouseButtonDown(0))
        {
            //PUT EMPTY SHOOT - SOUND HERE
        }

    }

}
