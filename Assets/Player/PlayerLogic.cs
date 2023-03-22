using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    public CharacterController2D controller;
    public int maxHeath = 100;
    int currentHealth;

    public float runSpeed = 40f;
    public int attackDamage = 40;
    float horizontalMove = 0f;
    bool jump = false;

    public Transform attackHitbox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDelay = 0.4f;
    public float attackTimer = 0f;

    public Animator animator;

    public HealthBar healthBar;

    public int playerScore = 0;
    public TextMeshProUGUI scoreText;

    void Start() {
        currentHealth = maxHeath;
        healthBar.SetMaxHeath(maxHeath);
    }

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

        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate() {
        // move character
        // false is for crouching input
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void Attack() {
        if (Time.time - attackTimer >= attackDelay) {
            attackTimer = Time.time;

            // throw out attackHitbox to detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.position, attackRange, enemyLayers);

            // damage enemies if in range of attack
            foreach(Collider2D enemy in hitEnemies) {
                Debug.Log(enemy.name + " was hit!");
                enemy.GetComponent<EnemyLogic>().TakeDamage(attackDamage);
                Flash enemyFlash = enemy.GetComponent<Flash>();
                if (enemyFlash != null)
                {
                    enemyFlash.Hit();
                }
            }
        }
    }

    public void PlayerTakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // TODO: play stagger animation

        // check if player died
        if (currentHealth <= 0) {
            PlayerDie();
        }
    }

    private void PlayerDie() {
        Debug.Log("Enemy Died!");
        // play death animation
        animator.SetBool("death", true);

        // disable player
        this.enabled = false;
    }

    public void AddScore() {
        playerScore += 15;
        scoreText.text = playerScore.ToString().PadLeft(9, '0');
    }

    void OnDrawGizmosSelected() {
        // this conditional is to avoid any errors
        if (attackHitbox == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackHitbox.position, attackRange);
    }
}
