using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera cam;
    public float spawnInterval;
    public float xPos = 1200f;
    public float yPos = 100f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
        GameObject newEnemy2 = Instantiate(enemy, new Vector3(cam.transform.position.x + -xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
