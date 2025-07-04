using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public int Count => _enemies.Length;

    public event Action<Vector2> Dead;

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Dead += SpawnReward;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Dead -= SpawnReward;
    }

    public void Respawn()
    {
        foreach(Enemy enemy in _enemies) 
            enemy.Respawn();
    }

    private void SpawnReward(Vector2 position)
    {
        Dead?.Invoke(position);
    }
}
