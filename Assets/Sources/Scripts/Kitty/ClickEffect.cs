using System;
using UnityEngine;

[Serializable]
public class ClickEffect 
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Play()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _particleSystem.Play();
        _particleSystem.transform.position = position;
    }
}