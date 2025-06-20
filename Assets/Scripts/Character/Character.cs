using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _animationController;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollisions _collisions;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Fliper _fliper;

    public event Action Dead;
    public event Action Respawned;

    public UserInput UserInput => _userInput;
    public GroundChecker GroundChecker => _groundChecker;
    public CharacterCollisions Collisions => _collisions;
    public CharacterMovement Movement => _movement;

    private void Update()
    {
        _fliper.SetHorizontalMoving(UserInput.HorizontalRaw);
    }

    private void OnEnable()
    {
        _collisions.Damaged += SetDead;
    }

    private void OnDisable()
    {
        _collisions.Damaged -= SetDead;
    }

    private void SetDead()
    {
        Dead?.Invoke();
    }

    public void Respawn()
    {
        Respawned?.Invoke();
    }
}
