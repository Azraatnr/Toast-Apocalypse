using UnityEngine;

// spawns zombie toasts from the left and right side of the screen and  also keeps track of waves
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval = 2f; // the seconds between each spawn
    [SerializeField] float spawnY = -3f;        // height where enemies spawn
    [SerializeField] float spawnX = 9f;         // how far offscreen enemies spawn

    int currentWave = 1;
    int maxWaves = 5;          // game ends after wave 5
    int enemiesPerWave = 3;    // starts at 3, goes up by 2 each wave
    int enemiesSpawned = 0;    // how many have been spawned this wave
    int enemiesKilled = 0;     // how many have been killed or reached finn this wave
    float timer = 0f;
    bool waveComplete = false; // stops spawning once all waves are done

    void Start()
    {
        GameManager.Instance.SetWave(currentWave); // show wave 1 on the ui from the start
    }

    void Update()
    {
        if (waveComplete) return;

        timer += Time.deltaTime;

        // only spawn if the timer is up and we havent spawned all enemies yet
        if (timer >= spawnInterval && enemiesSpawned < enemiesPerWave)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // randomly pick left or right side
        float side = Random.Range(0, 2) == 0 ? -spawnX : spawnX;
        Vector3 spawnPos = new Vector3(side, spawnY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesSpawned++;
    }

     // called when an enemy is destroyed  either killed by the player or when it reaches him.
    public void EnemyKilled()
    {
        enemiesKilled++;

        // checking if the whole wave is cleared
        if (enemiesKilled >= enemiesPerWave)
        {
            enemiesKilled = 0;
            enemiesSpawned = 0;
            currentWave++;

            if (currentWave > maxWaves)
            {
                // all 5 waves done, player wins
                GameManager.Instance.Win();
                waveComplete = true;
            }
            else
            {
                // next wave: spawn faster and more enemies
                spawnInterval *= 0.8f;
                enemiesPerWave += 2;
                GameManager.Instance.SetWave(currentWave);
            }
        }
    }
}