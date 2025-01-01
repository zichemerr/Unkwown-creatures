using DG.Tweening;
using UnityEngine;

public class KittyAnimation : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _transform;

    private Sequence _sequence;
    private Tween _tween;
    private Vector2 _position;

    public void Init()
    {
        _position = transform.position;
    }

    public void Play(int time)
    {
        if (_sequence != null)
            return;

        _sequence = DOTween.Sequence();
        _sequence.Append(_transform.DOShakePosition(time, 0.05f, 10).SetEase(Ease.InSine));
    }

    public void Move()
    {
        _tween = _transform.DOMove(_player.position, 3f);
    }

    public void StopMove()
    {
        _sequence.Kill();
        _sequence = null;
        _tween.Kill();
    }

    public void Stop()
    {
        _sequence.Kill();
        _sequence = null;
        _transform.DOMove(_position, 1f);
    }
}