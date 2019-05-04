using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class OnColorChangedEvent: UnityEvent<PlayerColorType>
{

}

public class PlayerController : MonoBehaviour
{
    public OnColorChangedEvent OnColorChanged = new OnColorChangedEvent();

    private Rigidbody2D _rigidBody;
    public float jumpForce = 7f;
    private Animator _animator;
    private Vector2 _initialRigidBodyPosition;
    [SerializeField]
    private PlayerColorType _playerColor;
    public PlayerColorType PlayerColor { get => _playerColor; private set => _playerColor = value; }

    [SerializeField]
    private PlayerMovementType _playerMovement;
    public PlayerMovementType PlayerMovement { get => _playerMovement; private set => _playerMovement = value; }

    public InputManager inputManager;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        gameController.OnGameStateChanged.AddListener(OnGameStateChanged);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _initialRigidBodyPosition = _rigidBody.position;
        TransitionPlayerColor(PlayerColorType.Green);
        TransitionPlayerMovement(PlayerMovementType.Idle);
        inputManager.OnKeyPressed.AddListener(OnKeyPressed);
    }

    private void OnGameStateChanged(GameStateType gameState)
    {
        switch (gameState)
        {
            case GameStateType.Running:
                enabled = true;
                break;
            default:
                enabled = false;
                break;
        }
    }

    private void OnKeyPressed(KeyAction action)
    {
        if (gameController.GameState == GameStateType.Running)
        {
            switch (action)
            {
                case KeyAction.ColorSwitch:
                    if (PlayerColor == PlayerColorType.Red)
                    {
                        TransitionPlayerColor(PlayerColorType.Green);
                    }
                    else
                    {
                        TransitionPlayerColor(PlayerColorType.Red);
                    }
                    break;
                case KeyAction.Jump:
                    if (PlayerMovement == PlayerMovementType.Idle)
                    {
                        TransitionPlayerMovement(PlayerMovementType.Jumping);
                        TimeSinceStartJump = 0f;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void Reset()
    {
        TransitionPlayerColor(PlayerColorType.Green);
        TransitionPlayerMovement(PlayerMovementType.Idle);
        _rigidBody.MovePosition(Vector2.zero);
        _rigidBody.MoveRotation(0f);
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0f;
    }
    float Vx;
    float Vy;
    public float JumpHeightUnits = 4f;
    public float JumpWidthUnits = 3.5f;

    private float TimeSinceStartJump = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((PlayerMovement == PlayerMovementType.Jumping))
        {
            float nextPositionY = JumpHeightUnits * Mathf.Sin(Mathf.Abs(gameController.GlobalGameMoveSpeed.x) * Mathf.PI / JumpWidthUnits * TimeSinceStartJump);
            Vector2 nextPosition = _rigidBody.position;
            nextPosition.y = _initialRigidBodyPosition.y + nextPositionY;
            if (nextPosition.y < _initialRigidBodyPosition.y)
            {
                TransitionPlayerMovement(PlayerMovementType.Idle);
                _rigidBody.MovePosition(_initialRigidBodyPosition);
            }
            else
            {
                _rigidBody.MovePosition(nextPosition);
            }
            TimeSinceStartJump += Time.fixedDeltaTime;
        }

        gameController.GameTimeScore += Time.fixedDeltaTime;

    }


    void TransitionPlayerMovement(PlayerMovementType newMovement)
    {
        if (PlayerMovement != newMovement)
        {
            Debug.Log("Transitioning player Color from" + PlayerMovement + " -> " + newMovement);
            PlayerMovement = newMovement;
            _animator.SetBool("IsJumping", newMovement == PlayerMovementType.Jumping);
        }
    }

    void TransitionPlayerColor(PlayerColorType newColor)
    {
        if (PlayerColor != newColor)
        {
            Debug.Log("Transitioning player Color from" + PlayerColor + " -> " + newColor);
            PlayerColor = newColor;
            _animator.SetBool("IsGreen", newColor == PlayerColorType.Green);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collision can passthrough
        ObstacleProperties obsProps = collision.gameObject.GetComponent<ObstacleProperties>();
        ObstacleMovement obsMove = collision.gameObject.GetComponent<ObstacleMovement>();
        if (obsProps != null)
        {
            if (IsCollided(obsProps.Color, PlayerColor))
            {
                Debug.Log(string.Format("Collided {0} -> {1}", PlayerColor, obsProps.Color));
                gameController.GameCollideScore += gameController.IncorrectCollideScore;
            }
            else
            {
                gameController.GameCollideScore += gameController.CorrectCollideScore;
                obsMove.SetHit();
            }
        }
    }

    private bool IsCollided(ObstacleColorType obstacleColor, PlayerColorType playerColor)
    {
        bool isCollide = false;

        if (playerColor == PlayerColorType.Green)
        {
            isCollide = (obstacleColor == ObstacleColorType.Black) || (obstacleColor == ObstacleColorType.Red);
        }
        else
        {
            isCollide = (obstacleColor == ObstacleColorType.Black) || (obstacleColor == ObstacleColorType.Green);
        }

        return isCollide;
    }
}
