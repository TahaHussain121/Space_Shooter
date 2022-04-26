using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPlayerAlive;
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
    void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        if (ObjectPool.InitializePool())
        {
            EnemySpawner.InitializeEnemySpawn();
        }

    }

}
