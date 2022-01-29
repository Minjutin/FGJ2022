using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    MonsterPathfinding monster;

    // Pit‰‰ kirjaa edellisest‰ Targetista
    GameObject previousTarget = null;

    void Start()
    {
        monster = FindObjectOfType<MonsterPathfinding>();
    }


    public void UpdatePreviousTarget(GameObject _reachedTarget)
    {
        Debug.Log("Previous Target: " + _reachedTarget);
        previousTarget = _reachedTarget;
    }

    public bool CheckIfSameTargetAsBefore(GameObject _previousTarget)
    {
        // Gives permission if possible next Target isn't previousTarget
        bool permissionToContinue;

        Debug.Log("PreviousTarget: " + previousTarget);

        // Compare possible nextTarget and previousTarget
        if (previousTarget == _previousTarget)
        {
            permissionToContinue = false;
        }
        else
        {
            permissionToContinue = true;
        }

        return permissionToContinue;
    }
}
