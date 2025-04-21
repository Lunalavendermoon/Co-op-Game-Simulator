using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    public EnemySpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetShooterState() {
        if (spawner.spawnInterval == 2f) {
            Enemy.ApplyToAllEnemies(e => e.speed = 1.5f);
            spawner.spawnInterval = 3f;
        }
        else {
            Enemy.ApplyToAllEnemies(e => e.speed = 2.5f);
            spawner.spawnInterval = 1f;
        }
    }
}
