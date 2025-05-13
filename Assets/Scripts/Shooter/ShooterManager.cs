using UnityEngine;
using Yarn.Unity;

public class ShooterManager : MonoBehaviour
{
    public static ShooterManager Instance { get; private set; }
    public DialogueRunner dialogueRunner;
    public EnemySpawner spawner;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject shooter;
    public GameObject gameover;
    public GameObject gameComplete;
    public GameObject dialogueOption;
    private float diffLv;
    private string currentState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<Bullet>().damage = 1f;
        spawner.UpdateSpawnInterval(3f);
        diffLv = 0;
    }

    public void SetShooterState(string difficulty) {
        if (difficulty == "easy") {
            Enemy.ApplyToAllEnemies(e => e.speed = 0.4f + diffLv/2);
            enemyPrefab.GetComponent<Enemy>().speed = 0.4f + diffLv/2;
            spawner.UpdateSpawnInterval(3f - diffLv);
            currentState = "easy";
        }
        else {
            Enemy.ApplyToAllEnemies(e => e.speed = 1.4f + diffLv);
            enemyPrefab.GetComponent<Enemy>().speed = 1.4f + diffLv;
            spawner.UpdateSpawnInterval(2f - diffLv/2);
            currentState = "hard";
        }
    }

    public void IncreaseDifficulty(float amt) {
        diffLv += amt;
        SetShooterState(currentState);
    }

    public void GameOver() {
        AudioManager.Instance.PlayBGM("gameover");
        dialogueOption.SetActive(false);
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("ShooterFail");
        shooter.SetActive(false);
        gameover.SetActive(true);
    }

    public void GameSuccess() {
        shooter.SetActive(false);
        gameComplete.SetActive(true);
    }
}
