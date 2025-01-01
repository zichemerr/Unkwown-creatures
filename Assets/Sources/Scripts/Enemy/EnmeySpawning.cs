using System;
using System.Collections;
using UnityEngine;

public class EnmeySpawning : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _catPrefab;
    [SerializeField] private SpawnSettings _spawnSettings;
    [SerializeField] private SpawnSettings _redSettings;

    private Player _playerDome;

    public event Action<Enemy> Spawned;

    public void Init(Player playerDome)
    {
        _playerDome = playerDome;
        StartCoroutine(SpawnEnemy(_enemyPrefab, _spawnSettings, 2));
        StartCoroutine(SpawnEnemy(_catPrefab, _redSettings, 5));
    }

    private IEnumerator SpawnEnemy(Enemy enemy, SpawnSettings spawnSettings, float duration)
    {
        yield return new WaitForSeconds(duration);
        Spawn(enemy, spawnSettings);
    }

    public Enemy Spawn(Enemy prefab, SpawnSettings spawnSettings)
    {
        Enemy enemy = Instantiate(prefab);
        enemy.transform.position = spawnSettings.Position;
        enemy.Init(spawnSettings, _playerDome);
        Spawned?.Invoke(enemy);

        return enemy;
    }
}
