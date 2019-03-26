using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    GameController _gameController;
    Rigidbody2D _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _gameController = GameController.GetGameController();
    }

    private void Update()
    {
        _rigidBody.velocity = _gameController.GlobalGameMoveSpeed;
        if (_rigidBody.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }

}
