using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSpawner : MonoBehaviour
{

    public GameObject chargerRoom;
    public List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int whichRoom = Random.Range(0, rooms.Count);
        Debug.Log(whichRoom);
        Instantiate(chargerRoom, rooms[whichRoom-1].transform.position, rooms[whichRoom].transform.rotation);
    }

}
