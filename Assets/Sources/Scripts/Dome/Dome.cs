using System;
using UnityEngine;

[Serializable]
public class Dome
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public Transform Transform;
    public SpriteRenderer SpriteRenderer;

    public float StartPosition => _startPosition.position.y;
    public float EndPosition => _endPosition.position.y;
}