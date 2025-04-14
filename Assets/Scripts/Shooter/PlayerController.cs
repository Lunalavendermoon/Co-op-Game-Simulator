using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public List<Transform> firePoints;
    public float shootInterval = 0.5f;
    private float shootTimer;
    private bool shootMore = false;

    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            if(shootMore) {
                Shoot(2);
            }
            else{ 
                Shoot(1);
            }
            shootTimer = 0f;
        }
    }

    void Shoot(int i)
    {
        switch(i) {
            case 1:
                Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity); 
                break;
            case 2:
                Instantiate(bulletPrefab, firePoints[1].position, Quaternion.identity);
                Instantiate(bulletPrefab, firePoints[2].position, Quaternion.identity);
                break;
            case 3:
                Instantiate(bulletPrefab, firePoints[3].position, Quaternion.identity);
                Instantiate(bulletPrefab, firePoints[4].position, Quaternion.identity);
                Instantiate(bulletPrefab, firePoints[4].position, Quaternion.identity);
                break;
        }
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // Trigger Game Over
        }
    }

    public void ShootMore() {
        shootMore = true;
    }
}
