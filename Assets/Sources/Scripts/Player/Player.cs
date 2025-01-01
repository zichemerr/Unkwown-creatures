using System.Collections;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private DomeAnimation _animation;
    [SerializeField] private PlayerAnimation _playerAnimaton;
    [SerializeField] private SoundsPlayer _soundsPlayer;
    [SerializeField] private SoundsPlayer _warning;
    [SerializeField] private AudioClip _air;
    [SerializeField] private AudioClip _think;
    [SerializeField] private AudioClip _warningClip;

    private Coroutine _coroutine;

    public bool IsDroped { get; private set; } = false;
    public bool IsAttacked = false;
    private bool _isActive;

    public event Action<string> Losed;

    public void Init()
    {
        _isActive = true;
        _animation.Init();
        _playerAnimaton.Init();
    }

    public void AttackStart()
    {
        if (IsAttacked == true)
            return;

        _warning.Play(0.5f);
        _animation.StartBlink();
        IsAttacked = true;
    }

    public void AttackFinish()
    {
        _warning.Stop();
        _animation.StopBlink();
        IsAttacked = false;
    }

    private void OnMouseDown()
    {
        if (_isActive == false)
            return;

        if (_animation.IsTweening == true)
            return;

        _soundsPlayer.Play(_think, 0.4f);

        if (IsDroped)
        {
            _animation.Raise();
            _playerAnimaton.Stop();
            StopCoroutine(_coroutine);
            IsDroped = false;
        }
        else
        {
            _animation.Drop();
            _coroutine = StartCoroutine(StartDeath());
            IsDroped = true;
        }
    }

    private IEnumerator StartDeath()
    {
        if (_isActive == false)
            yield break;

        _playerAnimaton.StartAnimation(8);
        yield return new WaitForSeconds(4);
        _soundsPlayer.Play(_air, 0.5f, true);
        yield return new WaitForSeconds(4);
        _soundsPlayer.Stop();

        if (_isActive == false)
            yield break;

        Losed?.Invoke("The air is out");
    }

    public void Disable()
    {
        _playerAnimaton.Disable();
        _animation.StopBlink();
        _isActive = false;
    }

    public void Break()
    {
        _animation._isBlink = true;
        _animation.Raise();
    }
}
