using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public float xBounds = 2f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-xBounds, xBounds);
        Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // Call this method to update the interval
    public void UpdateSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = StartCoroutine(SpawnLoop()); // restart loop with new interval
        }
    }
}
