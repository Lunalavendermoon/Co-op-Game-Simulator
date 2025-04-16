using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab, laserPrefab;
    public List<Transform> firePoints;
    public float shootInterval = 0.5f;
    private float shootTimer;
    private bool shootMore = false;

    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    private string shootingType = "";
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed * Time.deltaTime;
        }
        transform.Translate(moveX, 0, 0);

        if (Input.GetKey(KeyCode.B)) {
            if (shootingType == "") {
                shootingType = "laser";
            }
            else {
                
                GameObject laser = GameObject.FindWithTag("Bullet");
                if (laser != null)
                {
                    Destroy(laser);
                }
                shootingType = "";
            }
        }
        if (shootingType == ""){
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
        else {
            ShootOnce();
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

    void ShootOnce() {
        if (GameObject.Find("Laser(Clone)") == null) {
            Instantiate(laserPrefab, firePoints[0].position + Vector3.up * 6f, Quaternion.identity, transform);
        }
    }

    public void LoseHealth(int amount)
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

    public void SetShootInterval(int i) {
        shootInterval = i;
    }
}
