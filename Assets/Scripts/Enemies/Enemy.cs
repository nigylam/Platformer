using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int DieAnimation = Animator.StringToHash("Die");

    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _weekPlaceCollider;
    [SerializeField] private AudioSource _dieSound;

    public event Action<Vector2> Dead;
    public event Action Respawned;

    public float JumpPadForce { get; private set; } = 3f;

    public void Die()
    {
        _bodyCollider.enabled = false;
        Dead?.Invoke(transform.position);
        _dieSound.Play();
        _animator.Play(DieAnimation);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _animator.enabled = true;
        _bodyCollider.enabled = true;
        _weekPlaceCollider.enabled = true;
        Respawned?.Invoke();
    }
}
