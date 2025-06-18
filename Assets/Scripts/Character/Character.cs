using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterAnimations _animationController;
    [SerializeField] private CharacterMovement _controller;
    [SerializeField] private CharacterCollisions _collides;
    [SerializeField] private Game _game;

    public event Action Dead;
    public event Action Respawned;

    private void OnEnable()
    {
        _collides.Damaged += SetDead;
        _game.Restarted += Respawn;
    }

    private void OnDisable()
    {
        _collides.Damaged -= SetDead;
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
