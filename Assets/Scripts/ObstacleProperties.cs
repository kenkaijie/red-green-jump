using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    [SerializeField]
    private ObstacleColorType _color;
    public ObstacleColorType Color { get => _color; private set => _color = value; }
}
