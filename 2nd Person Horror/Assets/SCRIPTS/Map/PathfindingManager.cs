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

    [SerializeField] GameObject[] startingTargets;
    GameObject firstTarget;

    private void Awake()
    {
        firstTarget = startingTargets[Random.Range(0, startingTargets.Length)];
    }

    void Start()
    {
        monster = FindObjectOfType<MonsterPathfinding>();

        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        yield return new WaitForEndOfFrame();
        SpawnMonster();
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

    private void SpawnMonster()
    {
        // Disable navmesh Agent
        monster.SwitchNavMeshAgent(false);

        // Move Monster to starting location
        monster.transform.position = firstTarget.transform.position;

        // Set StartingTarget accordingly
        monster.currentTarget = firstTarget;

        // Enable navmesh Agent again
        monster.SwitchNavMeshAgent(true);
    }

    public GameObject FetchStartingTarget()
    {
        return firstTarget;
    }
}
