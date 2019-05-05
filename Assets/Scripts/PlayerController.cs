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
    private AudioSource _audioSource;
    private Vector2 _initialRigidBodyPosition;
    [SerializeField]
    private PlayerColorType _playerColor;
    public PlayerColorType PlayerColor { get => _playerColor; private set => _playerColor = value; }

    [SerializeField]
    private PlayerMovementType _playerMovement;
    public PlayerMovementType PlayerMovement { get => _playerMovement; private set => _playerMovement = value; }

    public InputManager inputManager;
    public GameController gameController;

    public List<AudioClip> JumpSounds;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        gameController.OnGameStateChanged.AddListener(OnGameStateChanged);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _initialRigidBodyPosition = _rigidBody.position;
        TransitionPlayerColor(PlayerColorType.Green);
        TransitionPlayerMovement(PlayerMovementType.Run);
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
                    if (PlayerMovement == PlayerMovementType.Run)
                    {
                        TransitionPlayerMovement(PlayerMovementType.Jump);
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
        TransitionPlayerMovement(PlayerMovementType.Run);
        _rigidBody.MovePosition(Vector2.zero);
        _rigidBody.MoveRotation(0f);
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0f;
    }

    private float _savedInitialYPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerMovement == PlayerMovementType.Jump)
        {
            _rigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(ListUtilities.TakeRandom(JumpSounds));
            TransitionPlayerMovement(PlayerMovementType.Jumping);
            
        }
        else if (PlayerMovement == PlayerMovementType.Jumping && _rigidBody.velocity.y < 0f)
        {
            TransitionPlayerMovement(PlayerMovementType.Falling);
        }
        else if (PlayerMovement == PlayerMovementType.Falling && Mathf.Abs(_rigidBody.velocity.y) < 0.01f)
        {
            _rigidBody.velocity = Vector2.zero;
            TransitionPlayerMovement(PlayerMovementType.Run);
        }
        else if (PlayerMovement == PlayerMovementType.Run && _rigidBody.velocity.y < 0f)
        {
            TransitionPlayerMovement(PlayerMovementType.Falling);
        }

            _velocityY = _rigidBody.velocity.y;

    }

    [SerializeField]
    private float _velocityY;

    void TransitionPlayerMovement(PlayerMovementType newMovement)
    {
        if (PlayerMovement != newMovement)
        {
            Debug.Log("Transitioning player Color from" + PlayerMovement + " -> " + newMovement);
            PlayerMovement = newMovement;
            _animator.SetBool("IsJumping",PlayerMovement != PlayerMovementType.Run);
        }
    }

    void TransitionPlayerColor(PlayerColorType newColor)
    {
        if (PlayerColor != newColor)
        {
            Debug.Log("Transitioning player Color from" + PlayerColor + " -> " + newColor);
            PlayerColor = newColor;
            _animator.SetBool("IsGreen", newColor == PlayerColorType.Green);
            if (PlayerColor == PlayerColorType.Green)
            {
                gameObject.layer = LayerMask.NameToLayer("PlayerGreen");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("PlayerRed");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "GameOverCollider")
        {
            gameController.TriggerGameOver();
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
