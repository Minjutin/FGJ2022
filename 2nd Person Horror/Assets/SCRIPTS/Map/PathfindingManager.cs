using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    MonsterPathfinding monster;

    // Pit‰‰ kirjaa edellisest‰ Targetista
    GameObject previousTarget = null;

    // Pit‰‰ listaa default restart Targeteista
    [SerializeField] GameObject[] defaultTargets;

    void Start()
    {
        monster = FindObjectOfType<MonsterPathfinding>();
    }


    public void UpdatePreviousTarget(GameObject _reachedTarget)
    {
        previousTarget = _reachedTarget;
    }

    public GameObject GetPreviousTarget()
    {
        return previousTarget;
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

        return permissionToContinue;
    }

    public GameObject GetRandomDefaultTarget()
    {
        var chosenTarget = defaultTargets[Random.Range(0, defaultTargets.Length)];
        return chosenTarget;
    }
}
