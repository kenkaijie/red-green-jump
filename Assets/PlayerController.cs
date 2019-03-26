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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.T) && PlayerColor != PlayerColorType.Green)
        {
            TransitionPlayerColor(PlayerColorType.Green);
            _spriteRenderer.sprite = GreenSprite;
        }
        else if (Input.GetKey(KeyCode.Y) && PlayerColor != PlayerColorType.Red)
        {
            TransitionPlayerColor(PlayerColorType.Red);
            _spriteRenderer.sprite = RedSprite;
        }
        else if ((PlayerMovement == PlayerMovementType.Jumping) && (_rigidBody.position.y <= 0.02f))
        {
            TransitionPlayerMovement(PlayerMovementType.Idle);
            _rigidBody.MovePosition(Vector2.zero);
        }
        else if ((Input.GetAxis("Vertical") > 0) && (PlayerMovement == PlayerMovementType.Idle))
        {
            TransitionPlayerMovement(PlayerMovementType.Jumping);
            _rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if ((Input.GetAxis("Vertical") < 0) && (PlayerMovement == PlayerMovementType.Jumping))
        {
            TransitionPlayerMovement(PlayerMovementType.Jumping);
            _rigidBody.AddForce(new Vector2(0, -0.2f*jumpForce), ForceMode2D.Impulse);
        }


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
                Debug.Log("Collided");
            }
        }
    }

    private bool IsCollided(ObstacleColorType obstacleColor, PlayerColorType playerColor)
    {
        bool returnValue = !((playerColor == PlayerColorType.Green) && (obstacleColor == ObstacleColorType.Green) ||
            (playerColor == PlayerColorType.Red) && (obstacleColor == ObstacleColorType.Red) ||
            (obstacleColor == ObstacleColorType.White));
        return returnValue;
    }
}
