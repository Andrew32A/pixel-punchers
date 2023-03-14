using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackHitbox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 0 === left click
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    void Attack() {
        // TODO: play attack animation

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
