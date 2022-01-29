using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPathfinding : MonoBehaviour
{
    public bool shouldIMove;
    public NavMeshAgent agent;

    [SerializeField] GameObject point;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (shouldIMove)
        {
            agent.SetDestination(point.transform.position);
        }
    }
}
