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

    // Start is called before the first frame update
    void Start()
    {
        spawnTimeCalculator = GetComponent<SpawnTimeCalculator>();
        timeToNextSpawn_s = TimeTillSpawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float randomValue = Random.value;

        if (timeToNextSpawn_s <=0f)
        {
            int randomIndex = Random.Range(0, DefaultPrefabList.Count);
            
            var obstacle = Instantiate(DefaultPrefabList[randomIndex], transform.position, Quaternion.identity, transform);
            ObstacleColorType randomColor = (ObstacleColorType)Random.Range(0, (int)ObstacleColorType.Count);
            obstacle.GetComponent<ObstacleProperties>().SetColor(randomColor);
            timeToNextSpawn_s = TimeTillSpawn();
            Debug.Log(string.Format("Spawning Obstacle Type: {0} ({1}), Next spawn in {2}s", randomIndex, randomColor, timeToNextSpawn_s));
        }
        timeToNextSpawn_s -= Time.fixedDeltaTime;
    }

    float TimeTillSpawn()
    {
        float retValue;
        if (spawnTimeCalculator is null)
        {
            retValue = holdbackTime_s + Random.Range(-holdbackTime_s / 2f, 2f * holdbackTime_s);
        }
        else
        {
            retValue = spawnTimeCalculator.CalculateSecondsToNextSpawn();
        }
        return retValue;
    }
}
