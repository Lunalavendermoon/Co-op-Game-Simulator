using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    public static ShooterManager Instance { get; private set; }
    public EnemySpawner spawner;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetShooterState() {
        if (spawner.spawnInterval == 1f) {
            Enemy.ApplyToAllEnemies(e => e.speed = 1.5f);
            spawner.UpdateSpawnInterval(3f);
        }
        else {
            Enemy.ApplyToAllEnemies(e => e.speed = 2.5f);
            spawner.UpdateSpawnInterval(1f);
        }
    }
}
