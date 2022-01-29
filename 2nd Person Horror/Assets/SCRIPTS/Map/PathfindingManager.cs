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
        previousTarget = _reachedTarget;
    }

    public bool CheckIfSameTargetAsBefore(GameObject _previousTarget)
    {
        // Gives permission if possible next Target isn't previousTarget
        bool permissionToContinue;

        // Compare possible nextTarget and previousTarget
        if (previousTarget == _previousTarget)
        {
            permissionToContinue = false;
        }
        else
        {
            permissionToContinue = true;
        }
        Debug.Log("permission: " + permissionToContinue);

        return permissionToContinue;
    }
}
