using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollisions _collisions;
    [SerializeField] private CharacterAnimations _animations;
    [SerializeField] private CharacterSounds _sounds;
    [SerializeField] private Fliper _fliper;

    public event Action Disabled;
    public event Action Dead;
    public event Action Respawned;

    public bool IsDisable { get; private set; } = false;

    public GroundChecker GroundChecker => _groundChecker;
    public CharacterCollisions Collisions => _collisions;

    private void Awake()
    {
        _movement.SetGroundChecker(_groundChecker);
        _collisions.SetGroundChecker(_groundChecker);
    }

    private void Update()
    {
        _fliper.SetHorizontalMoving(UserInput.HorizontalRaw);
        _animations.SetTriggers(_groundChecker.IsGrounded(), _collisions.IsStuned, _movement.RigidbodyVelocityY, _movement.RigidbodyVelocityX);
    }

    private void OnEnable()
    {
        _collisions.Damaged += _animations.SetDamaged;
        _collisions.Damaged += _sounds.PlayDamageSound;
        _movement.Jumped += _sounds.PlayJumpSound;
        _collisions.JumpEnemy += _movement.JumpEnemy;
    }

    private void OnDisable()
    {
        _collisions.Damaged -= _animations.SetDamaged;
        _collisions.Damaged -= _sounds.PlayDamageSound;
        _movement.Jumped -= _sounds.PlayJumpSound;
        _collisions.JumpEnemy -= _movement.JumpEnemy;
    }

    public void SetDisable()
    {
        Disabled?.Invoke();
        _movement.SetDisable();
        _sounds.DisableSound();
        _collisions.CancelStun();
        IsDisable = true;
    }

    public void SetDead()
    {
        SetDisable();
        _animations.SetDead();
        Dead?.Invoke();
    }

    public void Respawn()
    {
        Respawned?.Invoke();
        _movement.SetEnable();
        _sounds.EnableSound();
        _animations.SetRespawned();
        IsDisable = false;
    }

    public bool IsAvailable()
    {
        return IsDisable == false && _collisions.IsStuned == false;
    }
}
