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
    private GameObject candidateTarget = null;
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

            // Get the next Target
            //currentTarget = currentTarget.GetComponent<Target>().GiveNextTarget();

            //

            // What I want: 
            // A loop that goes on as until it gives a next Target
            // that is not the same as the previous one

            // Compare target list's chosen Target == previousTarget
            bool newTargetFound = false;
            while (newTargetFound == false)
            {
                candidateTarget = currentTarget.GetComponent<Target>().GiveNextTarget();
                var previousTarget = pathfindingManager.GetPreviousTarget();
                if (candidateTarget != previousTarget)
                {
                    newTargetFound = true;

                    Debug.Log("New Target Found!");
                    break;
                }
                else { newTargetFound = false; }
            }
            Debug.Log("Doing the stuff outside while loop");
            pathfindingManager.UpdatePreviousTarget(currentTarget);

            // Sets up the next Target
            currentTarget = candidateTarget;

            currentTargetPos = currentTarget.transform.position;
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
