using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int DieAnimation = Animator.StringToHash("Die");

    [Header("Links")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _weekPlaceCollider;
    [SerializeField] private AudioSource _dieSound;
    [SerializeField] private EnemyBody _body;
    [SerializeField] private EnemyWeakSpot _weakSpot;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private Health _health;

    [Header("Stats")]
    [SerializeField] private int _damage = 1;

    public event Action<Vector2> Dead;

    public float JumpPadForce { get; private set; } = 3f;

    private void OnEnable()
    {
        _body.PreparedForDisable += Disable;
        _weakSpot.Damaged += TakeDamage;
        _health.Dead += Die;
    }    
    
    private void OnDisable()
    {
        _body.PreparedForDisable -= Disable;
        _weakSpot.Damaged -= TakeDamage;
        _health.Dead -= Die;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _patrol.Respawn();
        _animator.enabled = true;
        _bodyCollider.enabled = true;
        _weekPlaceCollider.enabled = true;
        _body.Set(_damage);
    }

    private void TakeDamage(int damage)
    {
        _health.Decrease(damage);
    }

    private void Die()
    {
        Dead?.Invoke(transform.position);
        _bodyCollider.enabled = false;
        _patrol.Stop();
        _dieSound.Play();
        _animator.Play(DieAnimation);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
