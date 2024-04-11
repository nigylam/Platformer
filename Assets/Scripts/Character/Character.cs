using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action<bool> Dead;

    [SerializeField] private CharacterAnimationController _animationController;
    [SerializeField] private CharacterControler _controller;
    [SerializeField] private CharacterCollides _collides;
    [SerializeField] private Game _game;

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
        Dead?.Invoke(true);
    }

    public void Respawn()
    {
        Dead?.Invoke(false);
    }
}
