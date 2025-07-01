using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    public event Action Jumped;

    public float RigidbodyVelocityY { get => _rigidbody.velocity.y; }
    public float RigidbodyVelocityX { get => _rigidbody.velocity.x; }

    private bool _canDoSecondJump = true;
    private bool _isDisable = false;
    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody;
    private GroundChecker _groundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isDisable == false)
            _rigidbody.velocity = new Vector2(UserInput.HorizontalRaw * _speed, _rigidbody.velocity.y);
        else
            _rigidbody.velocity = Vector2.zero;
    }

    private void OnEnable()
    {
        UserInput.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        UserInput.JumpKeyPressed -= Jump;
    }

    public void SetGroundChecker(GroundChecker groundChecker)
    {
        _groundChecker = groundChecker;
    }

    public void SetDisable()
    {
        _isDisable = true;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    public void SetEnable()
    {
        _isDisable = false;
        transform.position = _startPosition;
    }

    public void JumpEnemy(EnemyWeakSpot _)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Jump()
    {
        if (CanJump() && _isDisable == false)
        {
            Jumped?.Invoke();
            _canDoSecondJump = _groundChecker.IsGrounded();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private bool CanJump() => _groundChecker.IsGrounded() || _canDoSecondJump;
}
