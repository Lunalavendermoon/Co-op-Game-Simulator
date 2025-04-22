using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> AllEnemies = new List<Enemy>();
    public static Dictionary<Enemy, Vector3> AllPositions = new Dictionary<Enemy, Vector3>();
    public float speed = 2f;
    public float maxHealth = 3;
    private float currentHealth;
    private PlayerController player;

    void Start()
    {
        AllEnemies.Add(this);
        AllPositions.Add(this, transform.position);
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        // TODO - idk if this line is necessary? are transforms updated in-place?
        AllPositions[this] = transform.position;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Obstacle"))
        {
            if (player != null)
            {
                player.LoseHealth(1);
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }

    public static void ApplyToAllEnemies(System.Action<Enemy> action)
    {
        foreach (var enemy in AllEnemies)
        {
            if (enemy != null) // Prevent null errors if any enemies were destroyed
            {
                action(enemy);
            }
        }
    }

    void OnDestroy()
    {
        AllEnemies.Remove(this);
        AllPositions.Remove(this);
    }
}
