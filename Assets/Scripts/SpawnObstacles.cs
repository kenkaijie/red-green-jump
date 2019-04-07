using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public float spawnSuccessRate = 0.01f;
    public List<GameObject> DefaultPrefabList;
    public float holdbackTime_s = 0.5f;
    private float timeToNextSpawn_s;

    SpawnTimeCalculator spawnTimeCalculator;

    public GameController gameController;

    private bool IsSpawningPaused = true;

    // Start is called before the first frame update
    void Start()
    {
        gameController.OnGameStateChanged.AddListener(OnGameStateChanged);
        IsSpawningPaused = true;
        spawnTimeCalculator = GetComponent<SpawnTimeCalculator>();
        timeToNextSpawn_s = TimeTillSpawn();
    }

    private void OnGameStateChanged(GameStateType gameState)
    {
        switch (gameState)
        {
            case GameStateType.Running:
                IsSpawningPaused = false;
                break;
            default:
                IsSpawningPaused = true;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsSpawningPaused)
        {
            float randomValue = UnityEngine.Random.value;

            if (timeToNextSpawn_s <= 0f)
            {
                int randomIndex = UnityEngine.Random.Range(0, DefaultPrefabList.Count);

                var obstacle = Instantiate(DefaultPrefabList[randomIndex], transform.position, Quaternion.identity, transform);
                obstacle.GetComponent<ObstacleMovement>().gameController = gameController;
                timeToNextSpawn_s = TimeTillSpawn();
                Debug.Log(string.Format("Spawning Obstacle Type: {0} ({1}), Next spawn in {2}s", randomIndex, obstacle.name, timeToNextSpawn_s));
            }
            timeToNextSpawn_s -= Time.fixedDeltaTime;
        }
    }

    float TimeTillSpawn()
    {
        float retValue;
        if (spawnTimeCalculator is null)
        {
            retValue = holdbackTime_s + UnityEngine.Random.Range(-holdbackTime_s / 2f, 2f * holdbackTime_s);
        }
        else
        {
            retValue = spawnTimeCalculator.CalculateSecondsToNextSpawn();
        }
        return retValue;
    }
}
