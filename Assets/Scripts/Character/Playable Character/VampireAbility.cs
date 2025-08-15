using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private int _hpStealingAmount = 1;
    [SerializeField] private float _stealIntervalSec = 2f;
    [SerializeField] private Health _health;

    private List<Enemy> _enemiesUnderAbility;
    private bool _isActive = true;
    private float _timer = 0f;

    private void Awake()
    {
        _enemiesUnderAbility = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemiesUnderAbility.Add(enemy);

        enemy.TakeDamage(_hpStealingAmount);
        _health.CanIncrease(_hpStealingAmount);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesUnderAbility.Remove(enemy);
    }

    public void Work()
    {
        if (_isActive && _enemiesUnderAbility.Count > 0)
        {
            _timer += Time.deltaTime;

            if (_timer >= _stealIntervalSec)
            {
                Debug.Log("stealed");
                StealHp();
                _timer = 0f;
            }
        }
    }

    private void StealHp()
    {
        Enemy enemy;

        if (_enemiesUnderAbility.Count == 1)
        {
            enemy = _enemiesUnderAbility[0];
        }
        else
        {
            enemy = GetNearestEnemy();
        }

        enemy.TakeDamage(_hpStealingAmount);
        _health.CanIncrease(_hpStealingAmount);
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
