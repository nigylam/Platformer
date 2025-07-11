using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterCollision _collision;
    [SerializeField] private CharacterAnimation _animation;
    [SerializeField] private CharacterSound _sound;
    [SerializeField] private CharacterAttacker _attacker;
    [SerializeField] private Health _health;
    [SerializeField] private CharacterMovement _movement;

    [Header("Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damageEnemyHeightDelta;

    public event Action Respawned;
    public event Action Dead;
    public event Action Healed;

    public CharacterCollision Collisions => _collision;

    private bool _isDisable = false;

    private void Start()
    {
        _movement.Set(_groundChecker, _speed, _jumpForce);
    }

    private void Update()
    {
        _animation.SetTriggers(_groundChecker.IsGrounded(), _collision.IsStuned, _movement.RigidbodyVelocityY, _movement.RigidbodyVelocityX);
    }

    private void OnEnable()
    {
        _collision.EnemyCollided += ReleaseEnemyCollision;
        _movement.Jumped += _sound.PlayJumpSound;
        _collision.MedkitCollided += Heal;
        _health.Dead += Die;
    }

    private void OnDisable()
    {
        _collision.EnemyCollided -= ReleaseEnemyCollision;
        _movement.Jumped -= _sound.PlayJumpSound;
        _collision.MedkitCollided -= Heal;
        _health.Dead -= Die;
    }

    public void SetDisable()
    {
        _movement.ChangeSpeed(0);
        _sound.DisableSound();
        _collision.CancelStun();
        _isDisable = true;
    }

    public void Respawn(int startHealth, int maxHealth)
    {
        Respawned?.Invoke();
        _health.Set(startHealth, maxHealth);
        _movement.ChangeSpeed(_speed);
        _movement.Reset();
        _sound.EnableSound();
        _animation.SetRespawned();
        _collision.CancelStun();
        _isDisable = false;
    }

    public bool IsAvailable()
    {
        return _isDisable == false && _collision.IsStuned == false;
    }

    private void ReleaseEnemyCollision(Enemy enemy, float heightDelta)
    {
        if(heightDelta <= _damageEnemyHeightDelta)
        {
            TakeDamage(enemy.Damage);
        }
        else
        {
            _attacker.Attack(enemy);
            _movement.JumpEnemy();
        }
    }

    private void TakeDamage(int damage)
    {
        _health.Decrease(damage);

        if (_health.Current > 0)
        {
            _animation.SetDamaged();
            _sound.PlayDamageSound();
            _collision.Stun();
        }
    }

    private void Heal(Medkit medkit)
    {
        if (_health.CanIncrease(medkit.Power))
        {
            medkit.Collect();
            Healed?.Invoke();
        }
    }

    private void Die()
    {
        _animation.SetDead();
        _sound.PlayDamageSound();
        SetDisable();
        _collision.ChangeLayer(true);
        Dead?.Invoke();
    }
}
