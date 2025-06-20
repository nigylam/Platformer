using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private float _closeDistanceToTarget = 2f;
    [SerializeField] private float _speed = 3f;

    private float _speedBeforeDeath;
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
    }

    private void Patrolling()
    {
        if (Vector2.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) <= _closeDistanceToTarget)
        {
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Length;
            _horizontalMoving = Mathf.Clamp(_patrolPoints[_currentPatrolPoint].position.x - transform.position.x, -1, 1);
        }

        _fliper.SetHorizontalMoving(_horizontalMoving);

        Move();
    }

    private void Move()
    {
        transform.Translate(_horizontalMoving * _speed * Time.deltaTime, 0f, 0f);
    }
}
