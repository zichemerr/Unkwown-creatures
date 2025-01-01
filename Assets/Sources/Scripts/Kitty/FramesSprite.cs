using System;
using UnityEngine;

[Serializable]
public struct FramesSprite
{
    [SerializeField] private Sprite[] _sprites;

    public int SpritesCount => _sprites.Length;

    public Sprite GetSprites(int index)
    {
        return _sprites[index];
    }
}