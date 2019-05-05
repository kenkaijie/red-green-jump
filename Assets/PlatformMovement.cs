using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;
    public GameController GameController { get { return _gameController; } set { _gameController = value; } }

    private void FixedUpdate()
    {
        if (_gameController != null)
        {
            transform.position += _gameController.GlobalGameMoveSpeed * Vector3.right * Time.fixedDeltaTime;
        }
    }
}
