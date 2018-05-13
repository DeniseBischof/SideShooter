using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    public GameObject EnemyType;
    public GameController changeLevel;

    [SerializeField]
    float maxSpawnRateInSeconds;

    public float Level = 1f;

    private Vector2 spawnPosition;


    // Use this for initialization
    void Start () {

//      StartEnemySpawn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy()
    {
        spawnPosition.x = 13f;
        spawnPosition.y = Random.Range(-4, 6);

        GameObject newEnemy = (GameObject)Instantiate(EnemyType);
        newEnemy.transform.position = new Vector2(spawnPosition.x, spawnPosition.y);

        NextEnemySpawn();
    
    }

    void NextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSeconds/ changeLevel.CurrentLevel > 0.2f)
        {
            spawnInNSeconds = Random.Range(1f / changeLevel.CurrentLevel, maxSpawnRateInSeconds/ changeLevel.CurrentLevel);
            Invoke("SpawnEnemy", spawnInNSeconds);
        }

        else
        {
            spawnInNSeconds = 0.2f;
            Invoke("SpawnEnemy", spawnInNSeconds);

        }

    }

    public void StartEnemySpawn()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    public void StopEnemySpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("NextEnemySpawn");

    }
}
