using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    public static ShooterManager Instance { get; private set; }
    public EnemySpawner spawner;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject shooter;
    public GameObject gameover;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().damage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetShooterState(string difficulty) {
        if (difficulty == "easy") {
            Enemy.ApplyToAllEnemies(e => e.speed = 0.5f);
            enemyPrefab.GetComponent<Enemy>().speed = 0.5f;
            spawner.UpdateSpawnInterval(3f);
        }
        else {
            Enemy.ApplyToAllEnemies(e => e.speed = 3f);
            enemyPrefab.GetComponent<Enemy>().speed = 3f;
            spawner.UpdateSpawnInterval(1f);
        }
    }

    public void GameOver() {
        AudioManager.Instance.PlayBGM("gameover");
        shooter.SetActive(false);
        gameover.SetActive(true);
    }
}
