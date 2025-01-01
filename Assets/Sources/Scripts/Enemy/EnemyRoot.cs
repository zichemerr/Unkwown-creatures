using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyRoot : MonoBehaviour
{
    [SerializeField] private EnmeySpawning _enemySpawning;
    [SerializeField] private List<Enemy> _enmies;
    [SerializeField] private bool _isSpawning;

    //Мяяяяяяяяяяя
    public TMP_Text text;

    public event Action<string> Losed;

    public void Init(Player playerDome)
    {
        if (_isSpawning == false)
            return;

        _enmies = new List<Enemy>();
        _enemySpawning.Init(playerDome);
    }

    private void OnEnable()
    {
        _enemySpawning.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _enemySpawning.Spawned -= OnSpawned;
    }

    private void OnSpawned(Enemy enemy)
    {
        _enmies.Add(enemy);
        enemy.Attacked += OnAttacked;
    }

    private void OnAttacked()
    {
        Losed?.Invoke("Defeat");
    }

    public void Disable()
    {
        foreach (var enemy in _enmies)
        {
            enemy.StopMove();
        }
    }
}