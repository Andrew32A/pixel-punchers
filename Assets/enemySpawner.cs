using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera cam;
    public float xPos = 1200f;
    public float yPos = 100f;

    public int wave = 0;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy(enemyPrefab);
    }

    private void spawnEnemy(GameObject enemy) {
        enemyCount += 2;
        wave += 1;
        GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
        GameObject newEnemy2 = Instantiate(enemy, new Vector3(cam.transform.position.x + -xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
    }

    private void nextWave() {
        if (enemyCount == 0) {
            spawnEnemy(enemyPrefab);
        }
    }

    public void waveCheck() {
        enemyCount -= 1;
        nextWave();
    }
}
