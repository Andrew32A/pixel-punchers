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
            Debug.Log("numclicks reset");
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
            Debug.Log("enemy attacked");
            // check for player in attack hitbox
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackHitbox.position, enemyAttackRange, playerLayers);

            foreach (Collider2D player in hitPlayer) {
                Debug.Log(player.name + " was hit!");
                player.GetComponent<PlayerLogic>().PlayerTakeDamage(enemyAttackDamage);
            }

            // play attack animation
            if (lastAttack) {
                numberClicks = 0;
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", false);
                Debug.Log("Enemy attack reset");
                lastAttack = false;
            }
            numberClicks++;
            lastClickTime = Time.time;

            if (numberClicks == 1) {
                animator.SetBool("attack1", true);
                Debug.Log("Enemy attack 1");
            } else if (numberClicks == 2) {
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", true);
                Debug.Log("Enemy attack 2");
            } else if (numberClicks == 3) {
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", true);
                lastAttack = true;
                Debug.Log("Enemy attack 3");
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
        // TODO: play death animation

        // disable enemy
        Destroy(gameObject, 0.0f);
        // GetComponent<BoxCollider2D>().enabled = false;
        // this.enabled = false;
        waveLogic.GetComponent<enemySpawner>().enemyCounter();
    }

    void OnDrawGizmosSelected() {
        // this conditional is to avoid any errors
        if (enemyAttackHitbox == null) {
            return;
        }
        Gizmos.DrawWireSphere(enemyAttackHitbox.position, enemyAttackRange);
    }
}
