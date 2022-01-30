using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject gunPoint;

    public bool canBeUsed = false;

    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {

        //If player decides to shoot.
        if (canBeUsed && Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, gunPoint.transform.position, gunPoint.transform.rotation);
            //PUT LOUD SOUND HERE

            canBeUsed = false;

        }

        if(!canBeUsed && Input.GetMouseButtonDown(0))
        {
            //PUT EMPTY SHOOT - SOUND HERE
        }

    }

}
