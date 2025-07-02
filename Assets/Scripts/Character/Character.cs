using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterCollisions _collisions;
    [SerializeField] private CharacterAnimations _animations;
    [SerializeField] private CharacterSounds _sounds;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private CharacterAttacker _attacker;

    public event Action Respawned;
    public event Action Dead;
    public event Action Healed;

    public int Health { get; private set; }
    public CharacterCollisions Collisions => _collisions;

    private bool _isDisable = false;
    private int _maxHealth;

    private void Awake()
    {
        _movement.SetGroundChecker(_groundChecker);
        _collisions.SetGroundChecker(_groundChecker);
    }

    private void Update()
    {
        _fliper.SetHorizontalMoving(UserInput.HorizontalRaw);
        _animations.SetTriggers(_groundChecker.IsGrounded(), _collisions.IsStuned, _movement.RigidbodyVelocityY, _movement.RigidbodyVelocityX);
    }

    private void OnEnable()
    {
        _collisions.EnemyCollided += GetDamage;
        _movement.Jumped += _sounds.PlayJumpSound;
        _collisions.EnemyWeakSpotCollided += _movement.JumpEnemy;
        _collisions.EnemyWeakSpotCollided += _attacker.Attack;
        _collisions.HealingCollided += Heal;
    }

    private void OnDisable()
    {
        _collisions.EnemyCollided -= GetDamage;
        _movement.Jumped -= _sounds.PlayJumpSound;
        _collisions.EnemyWeakSpotCollided -= _movement.JumpEnemy;
        _collisions.EnemyWeakSpotCollided -= _attacker.Attack;
        _collisions.HealingCollided -= Heal;
    }

    public void SetHealth(int startHealth, int maxHealth)
    {
        Health = startHealth;
        _maxHealth = maxHealth;
    }

    public void SetDisable()
    {
        _movement.SetDisable();
        _sounds.DisableSound();
        _collisions.CancelStun();
        _isDisable = true;
    }

    public void SetDead()
    {
        _animations.SetDead();
        _sounds.PlayDamageSound();
        SetDisable();
        _collisions.ChangeLayer(true);
        Dead?.Invoke();
    }

    public void Respawn()
    {
        Respawned?.Invoke();
        _movement.SetEnable();
        _sounds.EnableSound();
        _animations.SetRespawned();
        _collisions.CancelStun();
        _isDisable = false;
    }

    public bool IsAvailable()
    {
        return _isDisable == false && _collisions.IsStuned == false;
    }

    private void GetDamage()
    {
        Health--;

        if (Health > 0)
        {
            _animations.SetDamaged();
            _sounds.PlayDamageSound();
            _collisions.GetStuned();
        }
        else if (Health <= 0)
        {
            SetDead();
        }
    }

    private void Heal(Healing healing)
    {
        if (Health < _maxHealth)
        {
            healing.Collect();
            Health++;
            Healed?.Invoke();
        }
    }
}
