using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProperties : MonoBehaviour
{
    public float BaseHeight = 2f;

    [SerializeField]
    private float _platformWidth;
    public float PlatformWidth {
        get
        {
            return _platformWidth;
        }

        set {
            _platformWidth = value;
            if (_isStarted)
            {
                UpdateColliderProperties();
            }
        }
    }

    public ObstacleColorType _color;
    private EdgeCollider2D _edgeCollider;
    private SpriteRenderer _spriteRenderer;
    private PlatformEffector2D _platformEffector;
    private BoxCollider2D _boxCollider;
    private bool _isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _platformEffector = GetComponent<PlatformEffector2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        UpdateColliderProperties();
        _isStarted = true;
    }

    private void UpdateColliderProperties()
    {
        _spriteRenderer.size = new Vector2(_platformWidth, BaseHeight);
        _edgeCollider.points = new Vector2[] {
            new Vector2(-_platformWidth/2f, 0f),
            new Vector2(_platformWidth/2f, 0f),
        };
        _boxCollider.size = new Vector2(_platformWidth, 0.5f);

        var layers = new List<string>();

        if (_color == ObstacleColorType.Red || _color == ObstacleColorType.White)
        {
            layers.Add("PlayerRed");
        }

        if (_color == ObstacleColorType.Green || _color == ObstacleColorType.White)
        {
            layers.Add("PlayerGreen");
        }

        _platformEffector.colliderMask = LayerMask.GetMask(layers.ToArray());
    }
}
