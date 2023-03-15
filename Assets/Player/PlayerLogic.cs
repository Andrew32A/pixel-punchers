using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    public Transform attackHitbox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        // 0 === left click; might wanna change to KeyDown later for consistenency
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }

        // get user input (a, d, left arrow, right arrow)
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate() {
        // move character
        // false is for crouching input
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void Attack() {
        // play attack animation
        animator.SetTrigger("Attack");

        // throw out attackHitbox to detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.position, attackRange, enemyLayers);

        // damage enemies if in range of attack
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log(enemy.name + " was hit!");
        }
    }

    void OnDrawGizmosSelected() {
        // this conditional is to avoid any errors
        if (attackHitbox == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackHitbox.position, attackRange);
    }
}
