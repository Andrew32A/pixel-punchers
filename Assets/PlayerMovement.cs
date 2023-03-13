using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// pushing for funsies!!
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // get user input (a, d, left arrow, right arrow)
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate() {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        Debug.Log(jump);
    }
}
