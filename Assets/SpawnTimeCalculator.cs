using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimeCalculator : MonoBehaviour
{
    public float ConstantSpawnSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateSecondsToNextSpawn()
    {
        return ConstantSpawnSpeed;
    }
}
