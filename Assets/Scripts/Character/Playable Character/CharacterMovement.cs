using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : Movement
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public event Action Jumped;

    public float RigidbodyVelocityY => _rigidbody.velocity.y;
    public float RigidbodyVelocityX => _rigidbody.velocity.x;

    private bool _canDoSecondJump = true;
    private GroundChecker _groundChecker;
    private float _jumpForce;

    private void OnEnable()
    {
        UserInput.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        UserInput.JumpKeyPressed -= Jump;
    }

    private void FixedUpdate()
    {
        Move(UserInput.HorizontalRaw);
    }

    public override void Move(float horizontalMovement)
    {
        base.Move(horizontalMovement);

        _rigidbody.velocity = new Vector2(horizontalMovement * Speed, _rigidbody.velocity.y);
    }

    public void Set(GroundChecker groundChecker, float speed, float jumpForce)
    {
        _groundChecker = groundChecker;
        Speed = speed;
        _jumpForce = jumpForce;
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
