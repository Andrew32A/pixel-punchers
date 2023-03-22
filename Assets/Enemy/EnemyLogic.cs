using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int maxHeath = 100;
    int currentHealth;

    public GameObject player;
    public GameObject waveLogic;
    public LayerMask playerLayers;
    public bool flipSprite;
    public float enemySpeed;
    public int enemyDistance;
    public int enemyAttackDamage = 5;
    public float enemyAttackRange = 0.5f;
    public Transform enemyAttackHitbox;

    public Animator animator;
    public int numberClicks = 0;
    private float lastClickTime = 0;
    public float maxComboDelay = 2f;
    private bool lastAttack = false;
    public float attackTimer = 0f;
    public float attackDelay = 0.8f;

    void Start() {
        currentHealth = maxHeath;
    }

    void Update()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x + enemyDistance) {
            transform.Translate(enemySpeed * Time.deltaTime, 0, 0);
        } else if (player.transform.position.x < transform.position.x - enemyDistance){
            transform.Translate(enemySpeed * Time.deltaTime * -1, 0, 0);
        } else {
            EnemyAttack();
        }

        if (player.transform.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x) * -1 * (flipSprite ? -1 : 1);
        } else {
            scale.x = Mathf.Abs(scale.x) * (flipSprite ? -1 : 1);
        }

        transform.localScale = scale;

        if (Time.time - lastClickTime > maxComboDelay - (maxComboDelay / 2)) {
            numberClicks = 0;
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", false);
        }
    }

    public void EnemyAttack() {
        // check if enough time has passed since last attack
        if (Time.time - attackTimer >= attackDelay) {
            // reset attack timer
            attackTimer = Time.time;
            // check for player in attack hitbox
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackHitbox.position, enemyAttackRange, playerLayers);

            foreach (Collider2D player in hitPlayer) {
                player.GetComponent<PlayerLogic>().PlayerTakeDamage(enemyAttackDamage);
                Flash playerFlash = player.GetComponent<Flash>();
                if (playerFlash != null)
                {
                    playerFlash.Hit();
                }
            }

            // play attack animation
            if (lastAttack) {
                numberClicks = 0;
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", false);
                lastAttack = false;
            }
            numberClicks++;
            lastClickTime = Time.time;

            if (numberClicks == 1) {
                animator.SetBool("attack1", true);
            } else if (numberClicks == 2) {
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", true);
            } else if (numberClicks == 3) {
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", true);
                lastAttack = true;
            } 
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        // TODO: play stagger animation

        // check if enemy died
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Enemy Died!");
        // play death animation
        animator.SetBool("death", true);

        // disable enemy
        // Destroy(gameObject, 0.0f);
        this.enabled = false;

        // check if next wave should spawn
        waveLogic.GetComponent<enemySpawner>().waveCheck();

        // tell player that enemy died
        player.GetComponent<PlayerLogic>().AddScore();
    }

    void OnDrawGizmosSelected() {
        // this conditional is to avoid any errors
        if (enemyAttackHitbox == null) {
            return;
        }
        Gizmos.DrawWireSphere(enemyAttackHitbox.position, enemyAttackRange);
    }
}
