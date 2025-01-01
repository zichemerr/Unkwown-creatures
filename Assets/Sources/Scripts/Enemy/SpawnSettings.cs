using System;
using UnityEngine;

[Serializable]
public struct SpawnSettings
{
    [SerializeField] private Transform[] _points;

    [field: SerializeField] public float MinDelay { get; private set; }
    [field: SerializeField] public float MaxDelay { get; private set; }

    public Vector2 Position => _points[0].position;
    public float Count => _points.Length;

    public Vector2 GetPosition(int index)
    {
        return _points[index].position;
    }
}
