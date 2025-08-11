using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int DieAnimation = Animator.StringToHash("Die");

    [Header("Links")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private AudioSource _dieSound;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private Health _health;

    [Header("Stats")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _rewardSpawnHeight = -4;

    public event Action<Vector2> Dead;

    public int Damage => _damage;

    private void OnEnable()
    {
        _health.Dead += Die;
    }    
    
    private void OnDisable()
    {
        _health.Dead -= Die;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _health.Set();
        _patrol.Respawn();
        _animator.enabled = true;
        _collider.enabled = true;
    }

    public void TakeDamage(int damage = 1)
    {
        _health.Decrease(damage);
    }

    private void Die()
    {
        _collider.enabled = false;
        _patrol.Stop();
        _dieSound.Play();
        _animator.Play(DieAnimation);
    }

    public void OnAnimationEnding()
    {
        gameObject.SetActive(false);
        Dead?.Invoke(new Vector2(transform.position.x, _rewardSpawnHeight));
    }
}
