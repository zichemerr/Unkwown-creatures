using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private SpawnSettings _spawnSettings;
    private Queue<Vector2> _points;
    private Player _playerDome;
    private Coroutine _coroutine;
    private bool _isMoving;

    private float RandomValue
    {
        get
        {
            return Random.Range(_spawnSettings.MinDelay, _spawnSettings.MaxDelay);
        }
    }

    public event Action Attacked;

    public void Init(SpawnSettings spawnSettings, Player playerDome)
    {
        _spawnSettings = spawnSettings;
        _playerDome = playerDome;
        _isMoving = true;

        Clear();
        _points.Dequeue();
        StartCoroutine(StartMove());
    }
    
    private void Clear()
    {
        _points = new Queue<Vector2>();

        for (int i = 0; i < _spawnSettings.Count; i++)
        {
            _points.Enqueue(_spawnSettings.GetPosition(i));
        }
    }

    private void Active(bool a)
    {
        if (_isMoving == false)
            return;

        _spriteRenderer.enabled = a;
    }

    private IEnumerator StartMove()
    {
        if (_isMoving == false)
            yield break;

        if (_points.Count <= 0)
        {
            float time = 3;

            while (time > 0)
            {
                time -= Time.deltaTime;

                if (_isMoving == false)
                    yield break;

                if (_playerDome.IsDroped)
                    _playerDome.AttackFinish();
                else
                    _playerDome.AttackStart();

                yield return null;
            }

            if (_playerDome.IsDroped)
            {
                Clear();
                _coroutine = StartCoroutine(StartMove());
                _playerDome.AttackFinish();
                yield break;
            }

            if (_isMoving == false)
                yield break;

            Attacked?.Invoke();
            yield break;
        }

        if (_points.Count == _spawnSettings.Count)
        {
            Active(false);
        }

        yield return new WaitForSeconds(RandomValue);
        Active(false);
        yield return new WaitForSeconds(RandomValue);

        if (_isMoving == false)
            yield break;

        transform.position = _points.Dequeue();
        Active(true);
        _coroutine = StartCoroutine(StartMove());
    }

    public void StopMove()
    {
        _isMoving = false;
    }
}
