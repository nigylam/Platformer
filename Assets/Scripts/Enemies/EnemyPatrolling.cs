using System;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _closeDistanceToTarget = 2f;
    [SerializeField] private float _speed = 3f;

    private float _speedBeforeDeath;
    private bool _isFacingRight = false;
    private int _currentPatrolPoint = 0;
    private float _horizontalMoving = -1;

    private void OnEnable()
    {
        _enemy.Dead += Die;
        _enemy.Respawned += Respawn;
    }

    private void OnDisable()
    {
        _enemy.Dead -= Die;
        _enemy.Respawned -= Respawn;
    }

    private void Start()
    {
        _speedBeforeDeath = _speed;
    }

    private void Die()
    {
        _speed = 0;
    }

    private void Respawn()
    {
        _speed = _speedBeforeDeath;
    }

    private void Update()
    {
        Patrolling();
        Flip();
    }

    private void Patrolling()
    {
        if (Vector2.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) <= _closeDistanceToTarget)
        {
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Length;
            _horizontalMoving = Mathf.Clamp(_patrolPoints[_currentPatrolPoint].position.x - transform.position.x, -1, 1);
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(_horizontalMoving * _speed * Time.deltaTime, 0f, 0f);
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontalMoving < 0 || _isFacingRight == false && _horizontalMoving > 0)
        {
            _isFacingRight = !_isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
