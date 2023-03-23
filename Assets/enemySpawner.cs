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
        int numEnemies = 0;
        float[] xPosValues = new float[] { 1200f, 1400f }; // waves 1-3

        if (wave > 3) {
            numEnemies = wave - 2;
            xPosValues = new float[] { 1100f, 3000f }; // waves 4+
        } else {
            numEnemies = wave == 1 ? 1 : 2;
        }

        for (int i = 0; i < numEnemies; i++) {
            float xPos = Random.Range(xPosValues[0], xPosValues[xPosValues.Length - 1]);
            float xSign = i % 2 == 0 ? 1f : -1f;
            enemyCount += 1;

            GameObject newEnemy = Instantiate(enemy, new Vector3(cam.transform.position.x + xPos * xSign, cam.transform.position.y + yPos, 0), Quaternion.identity);
            increaseEnemyStats(newEnemy.GetComponent<EnemyLogic>());
        }
    }

    private void increaseEnemyStats(EnemyLogic enemy) {
        // increase enemy's health by 10% per wave
        enemy.maxHeath *= Mathf.Pow(1.1f, wave - 1);

        // increase enemy's speed
        enemy.enemySpeed *= Mathf.Pow(1.1f, wave - 1);
        
        // TODO: darken enemy's color by 10% per wave
        // enemy.GetComponent<SpriteRenderer>().color *= 1.9f; 
    }

    public void waveCheck() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            spawnEnemy(enemyPrefab);
            waveText.text = wave.ToString();
        }
    }
}
