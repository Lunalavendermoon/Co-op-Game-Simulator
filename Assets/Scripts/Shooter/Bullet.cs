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
            Vector3 targ = new Vector3(pos.x, pos.y - 1000, pos.z);
            foreach (var obj in Enemy.AllPositions)
            {
                if (obj.Key.IsDead()) {
                    continue;
                }
                var d = (pos - obj.Value).sqrMagnitude;
                if(d < dist)
                {
                    targ = obj.Value;
                    dist = d;
                }
            }

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            Vector3 lastDirection = Vector3.Normalize(targ - pos);
            float rotateAmount = Vector3.Cross(lastDirection, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
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
