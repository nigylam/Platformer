using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _closeDistanceToTarget = 2f;
    [SerializeField] private float _speedPatrolling = 2f;
    [SerializeField] private float _speedPursuing = 3f;
    [SerializeField] private float _pursuingDistance = 6f;
    [SerializeField] private float _pursuingVerticalDistance = 1f;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private GameObject _pursuitSign;

    private Fliper _fliper;
    private float _currentSpeed;
    private int _currentPatrolPoint = 0;
    private float _horizontalMoving = -1;
    private Vector2 _startPosition;
    private Character _character;
    private Vector2 _currentDestination;

    private void Awake()
    {
        _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        _startPosition = transform.position;
    }

    private void Start()
    {
        _currentSpeed = _speedPatrolling;
    }

    private void Update()
    {
        Patrolling();
    }

    public void SetLinks(Fliper fliper, Character character)
    {
        _fliper = fliper;
        _character = character;
    }

    public void Die(Vector2 position)
    {
        _pursuitSign.SetActive(false);
        _currentSpeed = 0;
    }

    public void Respawn()
    {
        _currentSpeed = _speedPatrolling;
        transform.position = _startPosition;
    }

    private void SetPursuit()
    {
        _currentSpeed = _speedPursuing;
        _pursuitSign.SetActive(true);
    }

    private void SetPatrolling()
    {
        _pursuitSign.SetActive(false);
        _currentSpeed = _speedPatrolling;
    }

    private void Patrolling()
    {
        if (CharacterIsNear())
        {
            SetPursuit();
            _currentDestination = new Vector2(_character.transform.position.x, transform.position.y);
        }

        if (Vector2.Distance(transform.position, _currentDestination) <= _closeDistanceToTarget)
        {
            SetPatrolling();
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPoints.Length;
            _currentDestination = _patrolPoints[_currentPatrolPoint].position;
        }

        Move();
    }

    private bool CharacterIsNear()
    {
        return Mathf.Abs(_character.transform.position.y - transform.position.y) <= _pursuingVerticalDistance 
            && Vector2.Distance(_character.transform.position, transform.position) <= _pursuingDistance 
            && _character.IsAvailable();
    }

    private void Move()
    {
        _horizontalMoving = Mathf.Clamp(_currentDestination.x - transform.position.x, -1, 1);
        _fliper.SetHorizontalMoving(_horizontalMoving);
        transform.Translate(_horizontalMoving * _currentSpeed * Time.deltaTime, 0f, 0f);
    }
}
