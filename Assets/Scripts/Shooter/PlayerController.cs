using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab, laserPrefab;
    public List<Transform> firePoints;
    public float shootInterval = 0.5f;
    private float shootTimer;
    public int shootAmt = 1;

    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    public string shootingType = "";
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

        if (shootingType == ""){
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                if(shootAmt == 2) {
                    Shoot(2);
                }
                else if (shootAmt == 3) { 
                    Shoot(3);
                }
                else {
                    Shoot(1);
                }
                shootTimer = 0f;
            }
        }
        else {
            if (GameObject.Find("Laser(Clone)") == null) {
                Instantiate(laserPrefab, firePoints[0].position + Vector3.up * 6f, Quaternion.identity, transform);
            }
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
                Instantiate(bulletPrefab, firePoints[5].position, Quaternion.identity);
                break;
        }
        
    }

    public void LoseHealth(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        Debug.Log("player lost health");
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // Trigger Game Over
        }
    }
}
