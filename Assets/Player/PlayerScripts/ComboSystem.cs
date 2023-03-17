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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.time - lastClickTime > maxComboDelay) 
        // {
        //     numberClicks = 0;
        // }
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
            // numberClicks = Mathf.Clamp(numberClicks, 0, 3); 
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
    public void return1() 
    {
    }
    public void return2() 
    {
    }
    public void return3() 
    {
    }
}
