using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComboSystem : MonoBehaviour
{
    public Animator animator;
    public int numberClicks = 0;
    private float lastClickTime = 0;
    public float maxComboDelay = 0.9f;
    private bool lastAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastClickTime > maxComboDelay) 
        {
            Debug.Log("numclicks reset");
            numberClicks = 0;
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", false);
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            if (lastAttack)
            {
                numberClicks = 0;
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", false);
                Debug.Log("Attack reset");
                lastAttack = false;
            }
            numberClicks++;
            lastClickTime = Time.time;
  
            if (numberClicks == 1) 
            {
                animator.SetBool("attack1", true);
                Debug.Log("Attack 1");
            }
            else if (numberClicks == 2) 
            {
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", true);
                Debug.Log("Attack 2");
            } 
            else if (numberClicks == 3) 
            {
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", true);
                lastAttack = true;
                Debug.Log("Attack 3");
            } 

        }
    }
}
