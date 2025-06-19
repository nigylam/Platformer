using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Характеристики передвижения")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    public event Action Jumped;

    public float RigidbodyVelocityY { get => _rigidbody.velocity.y; }
    public float RigidbodyVelocityX { get => _rigidbody.velocity.x; }

    private bool _canDoSecondJump = true;
    private bool _isDisable = false;
    private Vector2 _startPosition;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isDisable == false)
            _rigidbody.velocity = new Vector2(_character.UserInput.HorizontalRaw * _speed, _rigidbody.velocity.y);
    }

    private void OnEnable()
    {
        _character.UserInput.JumpKeyPressed += Jump;
        _character.Dead += SetDisable;
        _character.Respawned += SetEnable;
        _character.Collisions.JumpEnemy += JumpEnemy;
    }

    private void OnDisable()
    {
        _character.Dead -= SetDisable;
        _character.Respawned += SetEnable;
        _character.Collisions.JumpEnemy -= JumpEnemy;
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

    private void JumpEnemy()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Jump()
    {
        if (CanJump() && _isDisable == false)
        {
            Jumped?.Invoke();
            _canDoSecondJump = _character.GroundChecker.IsGrounded();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private bool CanJump() => _character.GroundChecker.IsGrounded() || _canDoSecondJump;
}
