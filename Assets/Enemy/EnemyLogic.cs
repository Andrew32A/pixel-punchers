using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int maxHeath = 100;
    int currentHealth;

    public GameObject player;
    public bool flipSprite;
    public float enemySpeed;

    void Start() {
        currentHealth = maxHeath;
    }

    void Update()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x) * -1 * (flipSprite ? -1 : 1);
            transform.Translate(enemySpeed * Time.deltaTime, 0, 0);
        } else {
            scale.x = Mathf.Abs(scale.x) * (flipSprite ? -1 : 1);
            transform.Translate(enemySpeed * Time.deltaTime * -1, 0, 0);
        }

        transform.localScale = scale;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // TODO: play stagger animation

        // check if enemy died
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Enemy Died!");
        // TODO: play death animation

        // disable enemy
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
