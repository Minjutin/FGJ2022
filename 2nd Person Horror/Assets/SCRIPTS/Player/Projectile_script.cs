using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_script : MonoBehaviour
{
    public float movementSpeed = 30f;

    void Update ()
    {
        gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

}
