using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    public string type = "";

    private Vector3 lastDirection = Vector3.up;

    void Start()
    {
        damage = 1f;
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
                var lookPos = targ - transform.position;
                lookPos.x = 0;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = rotation;

                lastDirection = Vector3.Normalize(targ - pos);
            }
            transform.Translate(lastDirection * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && type != "laser") {
            Destroy(gameObject);
        }
    }

    public void SetType(string i) {
        type = i;
    }

    void OnDestroy()
    {
        AudioManager.Instance.PlaySFX("pew");
    }
}
