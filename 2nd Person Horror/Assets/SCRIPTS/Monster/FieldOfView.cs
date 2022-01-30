using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    //Variables you know nothing about.
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    // Variables relating to other SCRIPTS
    public bool permissionToClearTargetsList = true;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] waypointTargets;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);

        //Invoke("AddWaypoints", 0.5f); // <-- Later functionality for waypoint sighting
    }

    //void AddWaypoints()
    //{
    //    waypointTargets = FindObjectsOfType<Target>();  // <-- Later functionality for waypoint sighting
    //}

    //Checking if there is targets around.
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    //Something about how to see targets. I don't actually remember anymore but I guess it's okay.
    void FindVisibleTargets()
    {
        // Disabled if searchMode is active (short search animation, looks for Player && Waypoint Targets)
        if (permissionToClearTargetsList)
        {
            visibleTargets.Clear();
        }


        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);



        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {

            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                // Checks if the line between Target and "camera" is not obstructed
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {

                    visibleTargets.Add(target);
                    //CheckTheVisibleTargetListForPlayer();
                }
            }
        }
    }

    //Check the list for Player
    public bool CheckTheVisibleTargetListForPlayer()
    {
        bool sighted = false;
        foreach (Transform visibleTarget in visibleTargets)
        {
            if (visibleTarget.position == player.transform.position)
            {
                sighted = true;
            }
        }

        if (sighted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    // Gets the Direction from the Angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    //void TestListOfVisibleTargets()
    //{
    //    foreach (Transform visibleTarget in fow.visibleTargets)
    //    {
    //        Handles.DrawLine(fow.transform.position, visibleTarget.position);
    //    }
    //}

}
