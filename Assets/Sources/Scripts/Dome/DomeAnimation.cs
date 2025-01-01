using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class DomeAnimation
{
    [SerializeField] private Dome _dome;

    private Tween _tween;
    [HideInInspector] public bool _isBlink;

    public bool IsTweening { get; private set; }
    public SpriteRenderer SpriteRenderer => _dome.SpriteRenderer;
    public Transform Transform => _dome.Transform;

    public void Init()
    {
        IsTweening = false;
    }

    private void Move(float positionY)
    {
        _tween.Kill();
        IsTweening = true;

        _tween = Transform.DOMoveY(positionY, 1);
        _tween.onComplete += () => IsTweening = false;
    }

    private void Blink()
    {
        if (_isBlink == false)
            return;

        SpriteRenderer.DOFade(0.5f, 0.1f)
            .onComplete += () => SpriteRenderer.DOFade(1f, 0.1f)
            .onComplete += () => Blink();
    }

    public void Drop()
    {
        Move(_dome.EndPosition);
    }

    public void Raise()
    {
        Move(_dome.StartPosition);
    }

    public void StartBlink()
    {
        _isBlink = true;
        Blink();
    }

    public void StopBlink()
    {
        _isBlink = false;
    }
}
