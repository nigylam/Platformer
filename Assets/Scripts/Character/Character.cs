using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollision _collision;
    [SerializeField] private CharacterAnimation _animation;
    [SerializeField] private CharacterSound _sound;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private CharacterAttacker _attacker;
    [SerializeField] private Health _health;

    public event Action Respawned;
    public event Action Dead;
    public event Action Healed;

    public CharacterCollision Collisions => _collision;

    private bool _isDisable = false;

    private void Awake()
    {
        _movement.SetGroundChecker(_groundChecker);
        _collision.SetGroundChecker(_groundChecker);
    }

    private void Update()
    {
        _fliper.SetHorizontalMoving(UserInput.HorizontalRaw);
        _animation.SetTriggers(_groundChecker.IsGrounded(), _collision.IsStuned, _movement.RigidbodyVelocityY, _movement.RigidbodyVelocityX);
    }

    private void OnEnable()
    {
        _collision.EnemyCollided += TakeDamage;
        _movement.Jumped += _sound.PlayJumpSound;
        _collision.EnemyWeakSpotCollided += _movement.JumpEnemy;
        _collision.EnemyWeakSpotCollided += _attacker.Attack;
        _collision.MedkitCollided += Heal;
        _health.Dead += Die;
    }

    private void OnDisable()
    {
        _collision.EnemyCollided -= TakeDamage;
        _movement.Jumped -= _sound.PlayJumpSound;
        _collision.EnemyWeakSpotCollided -= _movement.JumpEnemy;
        _collision.EnemyWeakSpotCollided -= _attacker.Attack;
        _collision.MedkitCollided -= Heal;
        _health.Dead -= Die;
    }

    public void SetDisable()
    {
        _movement.SetDisable();
        _sound.DisableSound();
        _collision.CancelStun();
        _isDisable = true;
    }

    public void Respawn(int startHealth, int maxHealth)
    {
        Respawned?.Invoke();
        _health.Set(startHealth, maxHealth);
        _movement.SetEnable();
        _sound.EnableSound();
        _animation.SetRespawned();
        _collision.CancelStun();
        _isDisable = false;
    }

    public bool IsAvailable()
    {
        return _isDisable == false && _collision.IsStuned == false;
    }

    private void TakeDamage(EnemyBody enemy)
    {
        _health.Decrease(enemy.Damage);

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
