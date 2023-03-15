using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistMovement : MonoBehaviour
{

    // Getting that commit badge baaaaby!
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("fist moved");
            Attack();
        }
    }

    void Attack() {
        transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
    }

    // void FixedUpdate() {
    //     transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
    // }
}
