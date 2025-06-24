using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _closeDistanceToTarget = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _pursuingDistance = 6f;
    [SerializeField] private float _pursuingVerticalDistance = 1f;
    [SerializeField] private Transform[] _patrolPoints;

    [SerializeField] private Vector2 _currentDestination;

    private Fliper _fliper;
    private Enemy _enemy;
    private float _speedBeforeDeath;
    private int _currentPatrolPoint = 0;
    private float _horizontalMoving = -1;
    private Vector2 _startPosition;

    private void Awake()
    {
        _fliper = GetComponent<Fliper>();
        _enemy = GetComponent<Enemy>();
        _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        _startPosition = transform.position;
    }

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
        transform.position = _startPosition;
    }

    private void Update()
    {
        Patrolling();
    }

    private void Patrolling()
    {
        if (CharacterIsNear())
        {
            _currentDestination = new Vector2(_character.transform.position.x, transform.position.y);
        }

        if (Vector2.Distance(transform.position, _currentDestination) <= _closeDistanceToTarget)
        {
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Length;
            _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        }

        Move();
    }

    private bool CharacterIsNear()
    {
        return Mathf.Abs(_character.transform.position.y - transform.position.y) <= _pursuingVerticalDistance &&
            Vector2.Distance(_character.transform.position, transform.position) <= _pursuingDistance && _character.IsDisable == false;
    }

    private void Move()
    {
        _horizontalMoving = Mathf.Clamp(_currentDestination.x - transform.position.x, -1, 1);
        _fliper.SetHorizontalMoving(_horizontalMoving);
        transform.Translate(_horizontalMoving * _speed * Time.deltaTime, 0f, 0f);
    }
}
