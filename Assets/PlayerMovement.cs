using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        // get user input (a, d, left arrow, right arrow)
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }

    void FixedUpdate() {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        Debug.Log(horizontalMove);
    }
}
