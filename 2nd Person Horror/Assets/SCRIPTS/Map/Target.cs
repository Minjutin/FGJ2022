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
        Debug.Log("Giving next target!");

        GameObject _nextTarget;

        _nextTarget = nextTargets[Random.Range(0, nextTargets.Length)];
        if (!pathManager.CheckIfSameTargetAsBefore(_nextTarget))
        {
            // Loop again
            GiveNextTarget();
        }

        // OLD VECTOR3 Return code
        //Vector3 nextTargetPos = _nextTarget.transform.position;
        //return nextTargetPos;

        return _nextTarget;
    }




}
