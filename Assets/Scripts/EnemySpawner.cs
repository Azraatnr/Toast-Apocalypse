using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnY = -3f;
    [SerializeField] float spawnX = 9f;

    int currentWave = 1;
    int maxWaves = 5;
    int enemiesPerWave = 3;
    int enemiesSpawned = 0;
    int enemiesKilled = 0;
    float timer = 0f;
    bool waveComplete = false;

    void Start()
    {
        GameManager.Instance.SetWave(currentWave);
    }

    void Update()
    {
        if (waveComplete) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval && enemiesSpawned < enemiesPerWave)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float side = Random.Range(0, 2) == 0 ? -spawnX : spawnX;
        Vector3 spawnPos = new Vector3(side, spawnY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesSpawned++;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesPerWave)
        {
            enemiesKilled = 0;
            enemiesSpawned = 0;
            currentWave++;

            if (currentWave > maxWaves)
            {
                GameManager.Instance.Win();
                waveComplete = true;
            }
            else
            {
                spawnInterval *= 0.8f;
                enemiesPerWave += 2;
                GameManager.Instance.SetWave(currentWave);
            }
        }
    }
}