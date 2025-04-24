using System.Collections;
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
    private Animator animator;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        AllEnemies.Add(this);
        AllPositions.Add(this, transform.position);
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
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
        StartCoroutine(GotHit());
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead){
            if (other.CompareTag("Player") || other.CompareTag("Obstacle"))
            {
                if (player != null)
                {
                    player.LoseHealth(1);
                }
                if (other.CompareTag("Player")){
                    KillEnemy();
                }
                else{
                    Destroy(gameObject);
                }
            }

            if (other.CompareTag("Bullet"))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                    if(bullet.type != "laser"){
                    Destroy(other.gameObject);}
                }
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

    public void KillEnemy() {
        if (!isDead) {
            isDead = true;
            animator.SetTrigger("explode");
            AudioManager.Instance.PlaySFX("explode");
            StartCoroutine(DestroyAfterAnim());
        }
    }

    void OnDestroy()
    {
        AllEnemies.Remove(this);
        AllPositions.Remove(this);
    }

    IEnumerator DestroyAfterAnim() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator GotHit() {
        AudioManager.Instance.PlaySFX("pew");
        spriteRenderer.color = new Color(1f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
