using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _closeDistanceToTarget = 1f;
    [SerializeField] private float _speedPatrol = 2f;
    [SerializeField] private float _speedPursuit = 3f;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyPursuit _pursuit;

    private int _currentPatrolPoint = 0;
    private Vector2 _currentDestination;
    private bool _isPatroling = true;

    private void Awake()
    {
        _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        _movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (_isPatroling)
            Patrolling();
    }

    public void Respawn()
    {
        _movement.ChangeSpeed(_speedPatrol);
        _movement.Reset();
        _isPatroling = true;
    }

    public void Stop()
    {
        _movement.ChangeSpeed(0);
        _isPatroling = false;
    }

    private void Patrolling()
    {
        if (_pursuit.CharacterIsNear())
        {
            _movement.ChangeSpeed(_speedPursuit);
            _currentDestination = new Vector2(_pursuit.Destination, transform.position.y);
        }

        if (transform.position.IsEnoughClose(_currentDestination, _closeDistanceToTarget))
        {
            _movement.ChangeSpeed(_speedPatrol);
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Length;
            _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        }

        _movement.Move(Mathf.Clamp(_currentDestination.x - transform.position.x, -1, 1));
    }

}
