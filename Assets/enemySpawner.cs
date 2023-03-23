using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera cam;
    public float xPos;
    public float yPos = 100f;

    public int wave = 0;
    private int enemyCount;

    public TextMeshProUGUI waveText;

    // start is called before the first frame update
    void Start()
    {
        spawnEnemy(enemyPrefab);
    }

    private void spawnEnemy(GameObject enemy) {
        wave += 1;

        if (wave == 1) {
            xPos = 1100f;
            enemyCount += 1;
            GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
            increaseEnemyStats(newEnemy.GetComponent<EnemyLogic>());
        } else if (wave < 5) {
            xPos = 1200f;
            enemyCount += 2;
            GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
            increaseEnemyStats(newEnemy.GetComponent<EnemyLogic>());
            xPos = 1400f;
            GameObject newEnemy2 = Instantiate(enemy, new Vector3(cam.transform.position.x + -xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
            increaseEnemyStats(newEnemy2.GetComponent<EnemyLogic>());
        } else {
            for (int i = 0; i < 10; i++){
                xPos = Random.Range(1100f, 3000f);
                enemyCount += 2;
                GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
                increaseEnemyStats(newEnemy.GetComponent<EnemyLogic>());
                GameObject newEnemy2 = Instantiate(enemy, new Vector3(cam.transform.position.x + -xPos, cam.transform.position.y + yPos, 0), Quaternion.identity);
                increaseEnemyStats(newEnemy2.GetComponent<EnemyLogic>());
            }
        }
    }

    private void nextWave() {
        if (enemyCount == 0) {
            spawnEnemy(enemyPrefab);
            waveText.text = wave.ToString();
        }
    }

    private void increaseEnemyStats(EnemyLogic enemy) {
        // increase enemy's health by 10% per wave
        enemy.maxHeath *= Mathf.Pow(1.1f, wave - 1);
        
        // darken enemy's color by 10% per wave
        // enemy.GetComponent<SpriteRenderer>().color *= 0.9f; 
    }

    public void waveCheck() {
        enemyCount -= 1;
        nextWave();
    }
}
