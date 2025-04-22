using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }
    public PlayerController player;
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

    public void BuffShootingSpd() {
        player.shootInterval /= 2f;
    }

    public void BuffShootingDmg() {
        Bullet.ApplyToAllBullets(b => b.damage *= 2f);
    }
    public void ActivateLaser()
    {
        StartCoroutine(EnableLaserTemporarily());
    }

    public void BuffShootingColumn() {
        player.shootAmt += 1;
    }

    public void ActivateBomb() {
        Enemy.ApplyToAllEnemies(enemy => GameObject.Destroy(enemy.gameObject));

    }
    public void RecoverHealth() {
        if (player.currentHealth < player.maxHealth) {
            player.LoseHealth(-1);
        }
    }

    private IEnumerator EnableLaserTemporarily()
    {
        player.shootingType = "laser";
        yield return new WaitForSeconds(10f);
        GameObject laser = GameObject.FindWithTag("Bullet");
        if (laser != null)
        {
            Destroy(laser);
        }
        player.shootingType = "";
    }

    public void ActivateHoming() {
        StartCoroutine(EnableHomingTemporarily());
    }

    private IEnumerator EnableHomingTemporarily()
    {
        player.shootingType = "homing";
        yield return new WaitForSeconds(10f);
        player.shootingType = "";
    }
}
