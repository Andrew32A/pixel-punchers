using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int maxHeath = 100;
    int currentHealth;

    public GameObject player;
    public LayerMask playerLayers;
    public bool flipSprite;
    public float enemySpeed;
    public int enemyDistance;
    public int enemyAttackDamage = 40;
    public float enemyAttackRange = 0.5f;
    public Transform enemyAttackHitbox;

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
    }

    public void EnemyAttack() {
        // throw out attackHitbox to detect player in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackHitbox.position, enemyAttackRange, playerLayers);

        // damage enemies if in range of attack
        foreach(Collider2D player in hitPlayer) {
            Debug.Log(player.name + " was hit!");
            player.GetComponent<PlayerLogic>().PlayerTakeDamage(enemyAttackDamage);
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
    }

    void OnDrawGizmosSelected() {
        // this conditional is to avoid any errors
        if (enemyAttackHitbox == null) {
            return;
        }
        Gizmos.DrawWireSphere(enemyAttackHitbox.position, enemyAttackRange);
    }
}
