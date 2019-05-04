using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float direction = -1f;
    public float scrollSpeed = 1f;
    public Vector3 centerPosition = Vector3.zero;

    public GameController gameController;

    [SerializeField]
    private float _spriteWidth;
    private bool _performMovement = true;

    private void Start()
    {
        centerPosition.z = transform.position.z;
        centerPosition.y = transform.position.y;
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x-0.02f*scrollSpeed;
        _performMovement = true;
        gameController?.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateType newState)
    {
        if (newState == GameStateType.Paused)
        {
            _performMovement = false;
        }
        else if (newState == GameStateType.Running)
        {
            _performMovement = true;
        }
    }

    private void Update()
    {
        if (_performMovement)
        {
            float movementX = (direction * scrollSpeed * Time.fixedDeltaTime);
            transform.position += movementX * Vector3.right;
            if ((transform.position - centerPosition).x < direction * _spriteWidth)
            {
                transform.position = centerPosition - direction * _spriteWidth * Vector3.right;
            }
        }
    }
}
