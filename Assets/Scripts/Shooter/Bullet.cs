using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 400f;
    public float damage = 1f;
    public string type = "";

    void Update()
    {
        if (type == "") {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (type == "homing") {
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

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (Enemy.AllPositions.Count != 0) {
                Vector3 lastDirection = Vector3.Normalize(targ - pos);
                // transform.Translate(lastDirection * speed * Time.deltaTime);
                float rotateAmount = Vector3.Cross(lastDirection, transform.up).z;

                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.velocity = transform.up * speed;
            } else {
                rb.angularVelocity = 0;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
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
}
