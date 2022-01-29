using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private PathfindingManager pathfindingManager;
    [Header("Targeting")]
    public Vector3 currentTargetPos;  //<-- SAME AS BELOW
    public GameObject currentTarget;
    [SerializeField] GameObject startingTarget;
    public bool shouldIMove;

    // Muuttujat
    [Header("Variables")]
    [SerializeField] float targetingTimer = 0.1f;
    public bool timeForTargetCheck = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingManager = FindObjectOfType<PathfindingManager>();

        // Sets the first Target up
        currentTarget = startingTarget;
        currentTargetPos = startingTarget.transform.position;
    }


    void Update()
    {
        Move();
        if (timeForTargetCheck)
        {
            StartCoroutine(CheckTargeting());
        }
    }

    private void Move()
    {
        if (shouldIMove)
        {
            agent.SetDestination(currentTargetPos);
        }
    }

    IEnumerator CheckTargeting()
    {
        timeForTargetCheck = false;

        // Check if Location of Monster and pathway Target is the same
        if (HasReachedCurrentTarget())
        {


            // Add current Target to list of previous Target(s)
            pathfindingManager.UpdatePreviousTarget(currentTarget);

            // Get the next Target
            currentTarget = currentTarget.GetComponent<Target>().GiveNextTarget();
            currentTargetPos = currentTarget.transform.position;
            Debug.Log("NEW TARGET ACQUIRED");
        }

        yield return new WaitForSeconds(targetingTimer);
        timeForTargetCheck = true;
    }

    private bool HasReachedCurrentTarget()
    {
        bool targetReached = true;

        // Gets the positions of both Self and Target without the Y coordinate
        var selfPos = new Vector3(transform.position.x, 0f, transform.position.z);
        var targetPos = new Vector3(currentTarget.transform.position.x, 0f, currentTarget.transform.position.z);

        if (selfPos == targetPos)
        {
            targetReached = true;
        }
        else { targetReached = false; }
        return targetReached;
    }
}
