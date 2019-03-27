using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class OnColorChangedEvent: UnityEvent<PlayerColorType>
{

}

public class PlayerController : MonoBehaviour
{
    public Sprite GreenSprite;
    public Sprite RedSprite;

    SpriteRenderer _spriteRenderer;

    private Sprite currentSprite;

    public OnColorChangedEvent OnColorChanged;

    private Rigidbody2D _rigidBody;
    public float jumpForce = 7f;

    [SerializeField]
    private PlayerColorType _playerColor;
    public PlayerColorType PlayerColor { get => _playerColor; private set => _playerColor = value; }

    [SerializeField]
    private PlayerMovementType _playerMovement;
    public PlayerMovementType PlayerMovement { get => _playerMovement; private set => _playerMovement = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        TransitionPlayerColor(PlayerColorType.Green);
        TransitionPlayerMovement(PlayerMovementType.Idle);
        currentSprite = GreenSprite;
        OnColorChanged = new OnColorChangedEvent();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (PlayerColor == PlayerColorType.Red)
            {
                TransitionPlayerColor(PlayerColorType.Green);
            }
            else
            {
                TransitionPlayerColor(PlayerColorType.Red);
            }
        }
        else if ((Input.GetAxis("Vertical") > 0) && (PlayerMovement == PlayerMovementType.Idle))
        {
            TransitionPlayerMovement(PlayerMovementType.Jumping);
            TimeSinceStartJump = 0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerColor == PlayerColorType.Red)
        {
            _spriteRenderer.sprite = RedSprite; 
        }
        else
        {
            _spriteRenderer.sprite = GreenSprite;
        }

        if ((PlayerMovement == PlayerMovementType.Jumping))
        {
            float nextPositionY = JumpHeightUnits * Mathf.Sin(Mathf.Abs(GameController.GetGameController().GlobalGameMoveSpeed.x) * Mathf.PI / JumpWidthUnits * TimeSinceStartJump);
            Vector2 nextPosition = _rigidBody.position;
            nextPosition.y = nextPositionY;
            if (nextPosition.y < 0f)
            {
                TransitionPlayerMovement(PlayerMovementType.Idle);
                Vector2 zeroPosition = _rigidBody.position;
                zeroPosition.y = 0f;
                _rigidBody.MovePosition(zeroPosition);
            }
            else
            {
                _rigidBody.MovePosition(nextPosition);
            }
            TimeSinceStartJump += Time.fixedDeltaTime;
        }

        GameController.GetGameController().GameTimeScore += Time.fixedDeltaTime;

    }


    void TransitionPlayerMovement(PlayerMovementType newMovement)
    {
        if (PlayerMovement != newMovement)
        {
            Debug.Log("Transitioning player Color from" + PlayerMovement + " -> " + newMovement);
            PlayerMovement = newMovement;
        }
    }

    void TransitionPlayerColor(PlayerColorType newColor)
    {
        if (PlayerColor != newColor)
        {
            Debug.Log("Transitioning player Color from" + PlayerColor + " -> " + newColor);
            PlayerColor = newColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collision can passthrough
        ObstacleProperties obsProps = collision.gameObject.GetComponent<ObstacleProperties>();

        if (obsProps != null)
        {
            if (IsCollided(obsProps.Color, PlayerColor))
            {
                Debug.Log(string.Format("Collided {0} -> {1}", PlayerColor, obsProps.Color));
                GameController.GetGameController().GameCollideScore += GameController.IncorrectCollideScore;
            }
            else
            {
                GameController.GetGameController().GameCollideScore += GameController.CorrectCollideScore;
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
