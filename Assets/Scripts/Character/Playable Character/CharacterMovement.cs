using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : Movement
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private GroundChecker _groundChecker;
    private UserInput _userInput;

    private bool _canDoSecondJump = true;
    private float _jumpForce;

    public event Action Jumped;

    public float RigidbodyVelocityY => _rigidbody.velocity.y;
    public float RigidbodyVelocityX => _rigidbody.velocity.x;

    private void OnEnable()
    {
        _userInput.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        _userInput.JumpKeyPressed -= Jump;
    }

    private void FixedUpdate()
    {
        Move(_userInput.HorizontalRaw);
    }

    public void Initialize(GroundChecker groundChecker, UserInput userInput, float speed, float jumpForce)
    {
        _groundChecker = groundChecker;
        _userInput = userInput;
        Speed = speed;
        _jumpForce = jumpForce;
    }

    public override void Move(float horizontalMovement)
    {
        base.Move(horizontalMovement);

        _rigidbody.velocity = new Vector2(horizontalMovement * Speed, _rigidbody.velocity.y);
    }


    public void JumpEnemy()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Jump()
    {
        if (CanJump())
        {
            Jumped?.Invoke();
            _canDoSecondJump = _groundChecker.IsGrounded();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private bool CanJump() => _groundChecker.IsGrounded() || _canDoSecondJump;
}
