using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public GameController gameController;
    Rigidbody2D _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidBody.velocity = gameController.GlobalGameMoveSpeed;
        if (_rigidBody.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }

}
