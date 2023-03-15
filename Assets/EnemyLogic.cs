using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public GameObject player;
    public bool flipSprite;
    public float enemySpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x) * -1 * (flipSprite ? -1 : 1);
        } else {
            scale.x = Mathf.Abs(scale.x) * (flipSprite ? -1 : 1);
        }

        transform.localScale = scale;
    }
}
