using System;
using UnityEngine;

[Serializable]
public class SoundsPlayer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    public void Play(float volume = 1, bool loop = false)
    {
        Play(_clip, volume, loop);
    }

    public void Play(AudioClip clip, float volume = 1, bool loop = false)
    {
        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}