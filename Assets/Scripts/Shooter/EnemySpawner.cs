using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public float xBounds = 2f;

    private Coroutine spawnCoroutine;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void OnEnable() {
        if (Instance == this) {
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }

    IEnumerator SpawnLoop() {
        yield return new WaitForSeconds(spawnInterval); // optional: delay first spawn
        while (true) {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy() {
        float randomX = Random.Range(-xBounds, xBounds);
        Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    public void UpdateSpawnInterval(float newInterval) {
        Debug.Log($"Updating spawn interval to {newInterval}");
        spawnInterval = newInterval;

        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }
}
