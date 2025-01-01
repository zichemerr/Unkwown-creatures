using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private Transform _transform;

    private Tween _tween;
    private Tween _snake;
    private Vector2 _startPosition;
    private bool _isSnake;
    private bool _isActive;

    public void Init()
    {
        _startPosition = _transform.position;
        _isActive = true;
    }

    private void Kill(Tween tween)
    {
        tween?.Kill();
    }

    public void StartAnimation(float time)
    {
        _tween = _player.DOColor(new Color(0, 0, 1), time).SetEase(Ease.InSine);
        _isSnake = true;
        StartCoroutine(StartSnake(time));
    }

    private IEnumerator StartSnake(float time)
    {
        if (_isActive == false)
            yield break;

        float delay = 4;

        while (delay > 0)
        {
            delay -= Time.deltaTime;

            if (_isSnake == false)
                yield break;

            yield return null;
        }

        _snake = _transform.DOShakePosition(time - 4, 0.05f, 10).SetEase(Ease.InSine);
    }

    public void Stop()
    {
        if (_tween == null)
            return; 

        Kill(_snake);
        Kill(_tween);
        _tween = null;
        _snake = null;
        _isSnake = false;
        _transform.DOMove(_startPosition, 1f);
        _player.DOColor(new Color(1, 1, 1), 1.5f);
    }

    public void Disable()
    {
        _isActive = false;
        Kill(_snake);
        Kill(_tween);
        _tween = null;
        _snake = null;
        _isSnake = false;
    }
}