using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    public Sprite RedSprite;
    public Sprite GreenSprite;
    public Sprite BlackSprite;
    public Sprite WhiteSprite;

    [SerializeField]
    private ObstacleColorType _color;
    public ObstacleColorType Color { get => _color; private set => _color = value; }

    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = GetSpriteFromColor(Color);
    }

    public void SetColor(ObstacleColorType color)
    {
        Color = color;
    }

    private Sprite GetSpriteFromColor(ObstacleColorType color)
    {
        Sprite returnSprite = null;
        switch (color)
        {
            case ObstacleColorType.Black:
                returnSprite = BlackSprite;
                break;
            case ObstacleColorType.Green:
                returnSprite = GreenSprite;
                break;
            case ObstacleColorType.Red:
                returnSprite = RedSprite;
                break;
            case ObstacleColorType.White:
                returnSprite = WhiteSprite;
                break;
        }
        return returnSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRenderer.sprite != GetSpriteFromColor(Color))
        {
            _spriteRenderer.sprite = GetSpriteFromColor(Color);
        }
    }
}
