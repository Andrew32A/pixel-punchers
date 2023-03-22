using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public Animator animator;
    public int numberClicks = 0;
    private float lastClickTime = 0;
    public float maxComboDelay = 0.9f;
    private bool lastAttack = false;
    public float attackDelay = 0.4f;
    public float attackTimer = 0f;

    void Update()
    {
        if (Time.time - lastClickTime > maxComboDelay) 
        {
            numberClicks = 0;
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", false);
        }
        if (Time.time - attackTimer >= attackDelay && Input.GetMouseButtonDown(0)) {
            attackTimer = Time.time;
            if (true == true) 
            {
                if (lastAttack)
                {
                    numberClicks = 0;
                    animator.SetBool("attack1", false);
                    animator.SetBool("attack2", false);
                    animator.SetBool("attack3", false);
                    lastAttack = false;
                }
                numberClicks++;
                lastClickTime = Time.time;
    
                if (numberClicks == 1) 
                {
                    animator.SetBool("attack1", true);
                }
                else if (numberClicks == 2) 
                {
                    animator.SetBool("attack1", false);
                    animator.SetBool("attack2", true);
                } 
                else if (numberClicks == 3) 
                {
                    animator.SetBool("attack2", false);
                    animator.SetBool("attack3", true);
                    lastAttack = true;
                } 
            }
        }
    }
}
