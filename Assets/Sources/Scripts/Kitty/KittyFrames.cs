using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KittyFrames
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FramesSprite _framesSprite;

    private Queue<Sprite> _sprites;

    public void Init()
    {
        ResetFrames();
        _spriteRenderer.sprite = _sprites.Dequeue();
    }

    private void ResetFrames()
    {
        _sprites = new Queue<Sprite>();

        for (int i = 0; i < _framesSprite.SpritesCount; i++)
            _sprites.Enqueue(_framesSprite.GetSprites(i));
    }

    private Sprite GetSprite()
    {
        if (_sprites.Count > 0)
            return _sprites.Dequeue();

        return null;
    }

    public Sprite Play()
    {
        Sprite sprite = GetSprite();

        if (sprite != null)
        {
            _spriteRenderer.sprite = sprite;
            return sprite;
        }

        return null;
    }
}
