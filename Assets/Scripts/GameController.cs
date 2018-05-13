using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    public GameObject Player;
    public GameObject bullet;
    public GameObject BossEnemy;

    public GameObject enemySpawner1;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;

    GameControllerState GCState;

    GameObject[] destroyObjects;

    [SerializeField]
    public GameObject GameOverUI;
    public GameObject GameTitleUI;

    [SerializeField]
    public Text Lives;
    const int MaxLives = 3;
    public int CurrentLives;

    [SerializeField]
    public Text LevelText;
    const int StartLevel = 1;
    public int CurrentLevel;
    private int BossLevel = 1;
    public static int BossLives = 20;

    [SerializeField]
    public Text ScoreTextGame;
    public Text ScoreTextGameOver;
    const int StartScore = 0;
    public static int Score;
    public int LevelUpScore;

    [SerializeField]
    public AudioClip play;
    public AudioClip gameBoss;
    public AudioClip start;
    public AudioClip restart;
    public AudioClip gameOverSound;
    AudioSource audioSource;

    [SerializeField]
    private GameObject Animation;

    private bool GameOverTrue = false;


    void SetStartingParameter()
    {
        CurrentLives = MaxLives;
        Lives.text = CurrentLives.ToString();

        CurrentLevel = StartLevel;
        LevelText.text = CurrentLevel.ToString();

        Score = StartScore;
        ScoreTextGame.text = Score.ToString();

        gameObject.SetActive(true);
    }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        BulletPool.Preload(bullet, 70);

        GCState = GameControllerState.Title;
        UpdateGameControllerState();

    }

    public enum GameControllerState
    {
        Title,
        StartGame,
        GamePlayNormal,
        GamePlayBoss,
        GameOver,
    }


    void Update()
    {

       ScoreTextGame.text = Score.ToString();
       Lives.text = CurrentLives.ToString();
       LevelText.text = CurrentLevel.ToString();
       LevelUpScore = Score / CurrentLevel;

        if (LevelUpScore >= 2000)
        {
            CurrentLevel++;
            BossLevel++;
            LevelUpScore = 0;

        }

        if (BossLevel == 3)
        {

            Invoke("BossFightStart", 0.2f);
            audioSource.PlayOneShot(gameBoss, 1F);
            BossLevel = 0;

        }

        if (BossLives <= 0)
        {
            BossLives = 20;
            audioSource.PlayOneShot(gameBoss, 1F);
            BossEnemy.SetActive(false);
            Invoke("StartGame", 0.2f);



        }

        if (GameOverTrue == true) { 
        if (Input.GetKeyDown(KeyCode.Space)) { //Thereza fragen wegen anderer Möglichkeit =/
            Invoke("StartGameTitle", 0.2f);
            audioSource.PlayOneShot(restart, 1F);
            GameOverTrue = false;
        }
        }

    }

    void UpdateGameControllerState () {
        switch (GCState)
        {

            case GameControllerState.Title:

                Player.transform.position = new Vector2(-8, 0);
                Player.SetActive(true);
                BossEnemy.SetActive(false);

                GameOverUI.SetActive(false);
                GameTitleUI.SetActive(true);
                SetStartingParameter();

                    Invoke("ResetGameState", 1f);

                break;

            case GameControllerState.StartGame:
                audioSource.PlayOneShot(start, 0.7F);

                Invoke("StartGame", 2f);

                break;

            case GameControllerState.GamePlayNormal:

                GameTitleUI.SetActive(false);

                enemySpawner1.GetComponent<EnemySpawner>().StartEnemySpawn();
                enemySpawner2.GetComponent<EnemySpawner>().StartEnemySpawn();
                enemySpawner3.GetComponent<EnemySpawner>().StartEnemySpawn();

                audioSource.clip = play;
                audioSource.Play();
    
                break;

            case GameControllerState.GamePlayBoss:

                destroyObjects = GameObject.FindGameObjectsWithTag("Enemies");

                for (var i = 0; i < destroyObjects.Length; i++)
                {

                    Destroy(destroyObjects[i]);
                }

                enemySpawner1.GetComponent<EnemySpawner>().StopEnemySpawn();
                enemySpawner2.GetComponent<EnemySpawner>().StopEnemySpawn();
                enemySpawner3.GetComponent<EnemySpawner>().StopEnemySpawn();

                BossEnemy.transform.position = new Vector2(6, 0);
                StartAnimationBoss();
                BossEnemy.SetActive(true);

                audioSource.PlayOneShot(gameBoss, 0.7F);


                break;

            case GameControllerState.GameOver:

                audioSource.Stop();

                audioSource.PlayOneShot(gameOverSound, 0.7F);
                enemySpawner1.GetComponent<EnemySpawner>().StopEnemySpawn();
                enemySpawner2.GetComponent<EnemySpawner>().StopEnemySpawn();
                enemySpawner3.GetComponent<EnemySpawner>().StopEnemySpawn();

                Player.SetActive(false);
                BossEnemy.SetActive(false);

                destroyObjects = GameObject.FindGameObjectsWithTag("Enemies");

                for (var i = 0; i < destroyObjects.Length; i++)
                {
                    Destroy(destroyObjects[i]);
                }

                ScoreTextGameOver.text = Score.ToString();
                GameOverUI.SetActive(true);

                GameOverTrue = true;

                break;

        }
	}

    public void SetGameController(GameControllerState state)
    {
        GCState = state;
        UpdateGameControllerState();
    }
    public void StartGameTitle()
    {
        GCState = GameControllerState.Title;
        UpdateGameControllerState();
    
    }

    public void ResetGameState()
    {

        GCState = GameControllerState.StartGame;
        UpdateGameControllerState();

    }

    public void StartGame()
    {

        GCState = GameControllerState.GamePlayNormal;
        UpdateGameControllerState();

    }

    public void BossFightStart()
    {

        GCState = GameControllerState.GamePlayBoss;
        UpdateGameControllerState();

    }

    void StartAnimationBoss()
    {
        GameObject gotHit = (GameObject)Instantiate(Animation);
        gotHit.transform.position = BossEnemy.transform.position;

    }
}
