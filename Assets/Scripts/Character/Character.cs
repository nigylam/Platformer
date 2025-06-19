using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _animationController;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollisions _collisions;
    [SerializeField] private Game _game;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;

    public event Action Dead;
    public event Action Respawned;

    public UserInput UserInput => _userInput;
    public GroundChecker GroundChecker => _groundChecker;
    public CharacterCollisions Collisions => _collisions;
    public CharacterMovement Movement => _movement;

    private void OnEnable()
    {
        _collisions.Damaged += SetDead;
        _game.Restarted += Respawn;
    }

    private void OnDisable()
    {
        _collisions.Damaged -= SetDead;
        _game.Restarted -= Respawn;
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
