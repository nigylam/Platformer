using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollisions _collisions;
    [SerializeField] private Fliper _fliper;

    public event Action Disabled;
    public event Action Dead;
    public event Action Respawned;

    public bool IsDisable { get; private set; } = false;

    public UserInput UserInput => _userInput;
    public GroundChecker GroundChecker => _groundChecker;
    public CharacterCollisions Collisions => _collisions;
    public CharacterMovement Movement => _movement;

    private void Update()
    {
        _fliper.SetHorizontalMoving(UserInput.HorizontalRaw);
    }

    public void SetDisable()
    {
        Disabled?.Invoke();
        _collisions.CancelStun();
        IsDisable = true;
    }

    public void SetDead()
    {
        SetDisable();
        Dead?.Invoke();
    }

    public void Respawn()
    {
        Respawned?.Invoke();
        IsDisable = false;
    }

    public bool IsAvailable()
    {
        return IsDisable == false && _collisions.IsStuned == false;
    }
}
