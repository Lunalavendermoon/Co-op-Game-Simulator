using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Obstacle"))
        {
            if (player != null)
            {
                player.TakeDamage(1);
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    void OnDestroy()
    {
        AudioManager.Instance.PlaySFX("pew");
    }
}
