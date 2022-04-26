using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public delegate void ValueUpdator(int x);

    public static event ValueUpdator _ScoreUpdate;
    public static event ValueUpdator _FinalScoreUpdate;

    public static event ValueUpdator _LiveUpdate;

    private GameStates gameState;
    [SerializeField]
    private int lives=6;
    [SerializeField]
    private int Score;
    [SerializeField]
    private const int maxLives = 6;

    public static GameManager Instance;

    public bool IsPlayerAlive;
    public void OnEnable()
    {
        gameState = GameStates.Start;
        GameUI._Start += InitializeGame;
        PlayerController._PLiveUpdate += UpdateLives;
        PlayerController._Dead += GameOver;
        EnemyController._PScoreUpdate += UpdateScore;
       

    }
    public void OnDisable()
    {
        GameUI._Start -= InitializeGame;
        PlayerController._PLiveUpdate -= UpdateLives;
        PlayerController._Dead -= GameOver;
        EnemyController._PScoreUpdate -= UpdateScore;


    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
    }

    private void UpdateScore()
    {
        Score += 10;
        Debug.Log("Score update " + Score);

        _ScoreUpdate(Score);
    }


    private void UpdateLives(int _lives)
    {

       lives=_lives;
        _LiveUpdate(_lives);
    }

    private void InitializeGame()
    {
        gameState = GameStates.GamePlay;

        if (ObjectPool.InitializePool())
        {
            EnemySpawner.InitializeEnemySpawn();
        }

    }
    private void GameOver()
    {
        Debug.Log("Gameover");
        gameState = GameStates.End;

        ShowFinalScore();
        EnemySpawner.EndEnemySpawn();
        ObjectPool._EndPool();

    }
    private void ShowFinalScore()
    {

        _FinalScoreUpdate(Score);
    }
}
