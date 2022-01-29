using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 selfPoint;
    PathfindingManager pathManager = null;

    [Header("Adjacent Targets")]
    public GameObject[] nextTargets;

    void Start()
    {
        selfPoint = transform.position;
        pathManager = FindObjectOfType<PathfindingManager>();
    }

    public GameObject GiveNextTarget()
    {

        GameObject _nextTarget;


        _nextTarget = nextTargets[Random.Range(0, nextTargets.Length)];
        // Crasher loop below? DO NOT USE
        //if (pathManager.CheckIfSameTargetAsBefore(_nextTarget))
        //{
        //    // Loop again
        //    GiveNextTarget();
        //}



        return _nextTarget;
    }




}
