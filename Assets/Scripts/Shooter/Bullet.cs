using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    public string type = "";
    public static List<Bullet> AllBullets = new List<Bullet>();

    bool isHoming;

    void Start()
    {
        AllBullets.Add(this);
    }
    void Update()
    {
        if (type == "") {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        } else if (type == "homing") {
            Vector3 pos = transform.position;
            float dist = float.PositiveInfinity;
            Vector3 targ = transform.position;
            foreach (var obj in Enemy.AllPositions)
            {
                var d = (pos - obj.Value).sqrMagnitude;
                if(d < dist)
                {
                    targ = obj.Value;
                    dist = d;
                }
            }
            if (Enemy.AllPositions.Count != 0) {
                transform.Translate(Vector3.Normalize(targ - pos) * speed * Time.deltaTime);
            } else {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (type == ""){
            Destroy(gameObject);}
        }
        if (other.CompareTag("Obstacle") && type == "") {
            Destroy(gameObject);
        }
    }

    public void SetType(string i) {
        type = i;
    }

    public static void ApplyToAllBullets(System.Action<Bullet> action)
    {
        foreach (var bullet in AllBullets)
        {
            if (bullet != null)
            {
                action(bullet);
            }
        }
    }
    void OnDestroy()
    {
        AudioManager.Instance.PlaySFX("pew");
        AllBullets.Remove(this);
    }
}
