using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public int maxHealth = 0;
    private List<GameObject> hearts = new List<GameObject>();

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart);
        }
    }

    public void SetHealth(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < health);
        }
    }
}
