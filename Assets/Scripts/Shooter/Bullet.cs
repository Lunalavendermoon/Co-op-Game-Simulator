using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public string type = "";

    void Update()
    {
        if (type == "") {transform.Translate(Vector3.up * speed * Time.deltaTime);}
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
}
