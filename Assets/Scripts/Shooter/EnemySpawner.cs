using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public float xBounds = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-xBounds, xBounds);
        Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
