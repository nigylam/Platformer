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
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private Character _character;
    [SerializeField] private Health _health;

    [Header("Stats")]
    [SerializeField] private int _damage = 1;

    public event Action<Vector2> Dead;

    public float JumpPadForce { get; private set; } = 3f;

    private void OnEnable()
    {
        _body.PreparedForDisable += Disable;
        _weakSpot.Damaged += GetDamage;
        _health.Dead += Die;
    }    
    
    private void OnDisable()
    {
        _body.PreparedForDisable -= Disable;
        _weakSpot.Damaged -= GetDamage;
        _health.Dead -= Die;
    }

    private void Awake()
    {
        _movement.SetLinks(_fliper, _character);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _movement.Respawn();
        _animator.enabled = true;
        _bodyCollider.enabled = true;
        _weekPlaceCollider.enabled = true;
        _body.Set(_damage);
    }

    private void GetDamage(int damage)
    {
        _health.Decrease(damage);
    }

    private void Die()
    {
        Dead?.Invoke(transform.position);
        _bodyCollider.enabled = false;
        _movement.Die(transform.position);
        _dieSound.Play();
        _animator.Play(DieAnimation);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
