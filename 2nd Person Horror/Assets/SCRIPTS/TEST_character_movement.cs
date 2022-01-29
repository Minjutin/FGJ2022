using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_character_movement : MonoBehaviour
{
    public float speed = 30f;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime));
    }
}
