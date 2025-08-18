using System.Collections.Generic;
using UnityEngine;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private int _hpStealingAmount = 1;
    [SerializeField] private float _stealIntervalSec = 2f;

    private bool _isActive = false;
    private float _timer = 0f;
    private List<Enemy> _enemiesUnderAbility;
    private Health _health;
    public void Initialize(Health health)
    {
        _enemiesUnderAbility = new List<Enemy>();
        _health = health;
    }
    
    public void Activate(bool isActive)
    {
        _isActive = isActive;
        _timer = _stealIntervalSec;
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemiesUnderAbility.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesUnderAbility.Remove(enemy);

        if (_enemiesUnderAbility.Count == 0)
            _timer = _stealIntervalSec;
    }

    public void Work()
    {
        if (_isActive && _enemiesUnderAbility.Count > 0)
        {
            _timer += Time.deltaTime;

            if (_timer >= _stealIntervalSec)
            {
                StealHp();
                _timer = 0f;
            }
        }
    }

    private void StealHp()
    {
        Enemy enemy;

        if (_enemiesUnderAbility.Count == 1)
            enemy = _enemiesUnderAbility[0];
        else
            enemy = GetNearestEnemy();

        enemy.TakeDamage(_hpStealingAmount);
        _health.TryIncrease(_hpStealingAmount);
    }

    private Enemy GetNearestEnemy()
    {
        float closestDistanceSqr = float.MaxValue;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in _enemiesUnderAbility)
        {
            float distanceScr = Vector3Extensions.SqrDistance(gameObject.transform.position, enemy.transform.position);

            if (closestDistanceSqr > distanceScr)
            {
                nearestEnemy = enemy;
                closestDistanceSqr = distanceScr;
            }
        }

        return nearestEnemy;
    }
}
