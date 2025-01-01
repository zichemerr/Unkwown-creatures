using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private float _time;

    private bool _isActive;

    public event Action<string> Winned;

    public void Init()
    {
        _isActive = true;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        if (_isActive == false)
            yield break;

        while (_time > 0)
        {
            if (_isActive == false)
                yield break;

            _time -= Time.deltaTime;
            int time = (int)_time;
            _Text.text = time.ToString();
            yield return null;
        }

        Winned?.Invoke("Victory");
    }

    public void Disable()
    {
        _isActive = false;
    }
}
