using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int spawnrate = 5;

  private static EnemySpawner Instance;
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
    public static void InitializeEnemySpawn()
    {
        Instance._InitializeEnemySpawn();
    }

    private  void _InitializeEnemySpawn()
    {
        Instance.Spawn();
        InvokeRepeating("IncreaseDifficulty", 5, 5.0f);
    }

    public static void EndEnemySpawn()
    {

        Debug.Log("Shutting spawner");

        Instance._EndEnemySpawn();
      
    }
    private void _EndEnemySpawn()
    {
        CancelInvoke("Spawn");
        CancelInvoke("ScheduleSpawning");
        CancelInvoke("IncreaseDifficulty");
    }

    private void Spawn()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject enemy = ObjectPool.GetPooledObject(ItemType.Enemy);
        if (enemy != null)
        {
            enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            enemy.SetActive(true);
        }
        ScheduleSpawning();


    }

    private void IncreaseDifficulty()
    {
        if (spawnrate > 1)
        {
            spawnrate = spawnrate - 1;
        }
        else
        {
            CancelInvoke("IncreaseDifficulty");
        }
       
    }

    private void ScheduleSpawning()
    {
        float spawnRate = 1;
        if (spawnrate > 1)
        {
            spawnRate = Random.RandomRange(1, spawnRate);
        }
        Invoke("Spawn", spawnrate);

        
    }




}
