using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public Vector2 GlobalGameMoveSpeed = new Vector2(-9f, 0);

    private static GameController instance;
    public static GameController GetGameController()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
