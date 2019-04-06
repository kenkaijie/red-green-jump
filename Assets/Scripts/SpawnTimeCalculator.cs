using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimeCalculator : MonoBehaviour
{
    public float ConstantSpawnSpeed = 0.5f;

    public float CalculateSecondsToNextSpawn()
    {
        return ConstantSpawnSpeed;
    }
}
