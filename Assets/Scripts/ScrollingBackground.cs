using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float direction = -1f;
    public float scrollSpeed = 1f;
    public Vector3 centerPosition = Vector3.zero;

    [SerializeField]
    private float _spriteWidth = 0f;
    private bool _doMovement = false;

    public void SetMotionState(bool doMovement)
    {
        _doMovement = doMovement;
    }

    private void Start()
    {
        centerPosition.z = transform.position.z;
        centerPosition.y = transform.position.y;
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (_doMovement)
        {
            float movementX = (direction * scrollSpeed * Time.fixedDeltaTime);
            transform.position += movementX * Vector3.right;
            if ((transform.position - centerPosition).x < direction * _spriteWidth)
            {
                transform.position += -3f * direction * _spriteWidth * Vector3.right;
            }
        }
    }
}
