using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;

public class RunnerLevelManager : MonoBehaviour
{
    float point = 0;
    float highestPoint = 0;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI warnText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        highestPoint = PlayerPrefs.GetInt("highestPoint", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            warnText.text = "You died, 'R' to restart";
            scoreText.text = "";

            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetInt("highestPoint", (int)highestPoint);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            if (point > highestPoint)
            {
                highestPoint = point;
            }
            point += 1 * Time.deltaTime;
            scoreText.text = "High: " + highestPoint.ToString("N0") + "                Score: " + point.ToString("N0"); ;
        }
    }
}
