using System;
using System.Collections;
using UnityEngine;

public class Kitty : MonoBehaviour
{
    [SerializeField] private KittyFrames _kittyFrames;
    [SerializeField] private ClickEffect _clickEffect;
    [SerializeField] private KittyAnimation _kittyAnimation;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _view;
    [SerializeField] private SoundsPlayer _soundPlayer;
    [SerializeField] private AudioClip _warning;
    [SerializeField] private AudioClip _click;
    [SerializeField] private Vector2 _scale;
    [SerializeField] private float _duration;

    private Coroutine _coroutine;
    private ClickAnimation _clickAnimation;
    private bool _isActive;
    private int _interations;

    public event Action<string> Losed;

    public void Init()
    {
        _isActive = true;
        _kittyFrames.Init();
        _coroutine = StartCoroutine(StartDelay());
    }

    private void Awake()
    {
        _clickAnimation = new ClickAnimation(_view, _scale, _duration);
        _clickAnimation.Reset();
        _kittyAnimation.Init();
    }

    private void OnMouseDown()
    {
        if (_isActive == false)
            return;

        _interations = 0;
        _kittyAnimation.Stop();
        _soundPlayer.Play(_click, 0.3f);
        _clickAnimation.Play();
        _clickEffect.Play();
        StopCoroutine(_coroutine);
        Init();
    }

    private IEnumerator StartDelay()
    {
        if (_isActive == false)
            yield break;

        yield return new WaitForSeconds(4);

        if (_isActive == false)
            yield break;

        Sprite sprite = _kittyFrames.Play();
        _interations++;

        if (_interations > 3)
        {
            _soundPlayer.Play(_warning, 1f, true);
            _kittyAnimation.Play(6);
            _interations = 0;
        }

        if (sprite == null)
        {
            _isActive = false;
            _kittyAnimation.Move();
            _player.Break();
            yield return new WaitForSeconds(1.5f);
            _kittyAnimation.StopMove();
            _soundPlayer.Stop();
            Losed?.Invoke("Not enough love");
        }

        _coroutine = StartCoroutine(StartDelay());
    }

    public void Disable()
    {
        _isActive = false;
    }
}
