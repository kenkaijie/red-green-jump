using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public GameController gameController;
    private Rigidbody2D _rigidBody;
    private Animator _animator;


    [SerializeField]
    private bool _isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        _isHit = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isHit)
        {
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.velocity = gameController.GlobalGameMoveSpeed;
        }
        if (_rigidBody.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }

    public void SetHit()
    {
        _animator.SetBool("IsHit", true);
        _isHit = true;
        Destroy(gameObject, 0.5f);
    }

}
