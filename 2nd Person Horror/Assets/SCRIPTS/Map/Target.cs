using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 selfPoint;

    [Header("Adjacent Targets")]
    public GameObject[] nextTargets;

    void Start()
    {
        selfPoint = transform.position;
    }

    public Vector3 GiveNextTarget()
    {
        new Vector3 next;
        return nextTargets;
    }




}
