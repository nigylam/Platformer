using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour
{
    [Header("Character stats")]
    [SerializeField] private int _characterStartHealth;
    [SerializeField] private int _characterMaxHealth;

    [Header("Links")]
    [SerializeField] private Character _character;
    [SerializeField] private EnemySpawner _enemies;
    [SerializeField] private CollectableSpawner _gemSpawner;
    [SerializeField] private CollectableSpawner _healingSpawner;

    public int GemsMax => _gemsMax;


    private int _characterHealth;
    private int _gemsMax;
    private int _gemsCount;
    private bool _isGameEnd = false;


    public event Action Won;
    public event Action Over;
    public event Action<int> GemsCountChanged;
    public event Action Restarted;

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (_characterHealth == 0 && _isGameEnd == false)
        {
            _isGameEnd = true;
            End(false);
            return;
        }

        if (_gemsCount == _gemsMax && _isGameEnd == false)
        {
            _isGameEnd = true;
            End(true);
        }
    }

    private void OnEnable()
    {
        _character.Collisions.Damaged += GetDamage;
        _character.Collisions.GemCollected += CollectGem;
        _character.Collisions.HealingCollected += CollectHealing;
        _enemies.Dead += SpawnReward;
    }

    private void OnDisable()
    {
        _character.Collisions.Damaged -= GetDamage;
        _character.Collisions.GemCollected -= CollectGem;
        _character.Collisions.HealingCollected -= CollectHealing;
    }

    public void Restart()
    {
        _gemSpawner.Despawn();
        _healingSpawner.Despawn();
        _characterHealth = _characterStartHealth;
        _gemsCount = 0;
        _gemsMax = _gemSpawner.Count + _enemies.Count;
        _isGameEnd = false;
        Restarted?.Invoke();
        _character.Respawn();
        _enemies.Respawn();
        _gemSpawner.Spawn();
        _healingSpawner.Spawn();
    }

    private void End(bool isWin)
    {

        if (isWin)
        {
            _character.SetDisable();
            Won?.Invoke();
        }

        else
        {
            _character.SetDead();
            Over?.Invoke();
        }
    }

    private void CollectGem()
    {
        _gemsCount++;
        GemsCountChanged?.Invoke(_gemsCount);
        _gemSpawner.Sounds.Play();
    }

    private void CollectHealing()
    {
        _characterHealth++;
        _healingSpawner.Sounds.Play();
    }

    private void GetDamage()
    {
        _characterHealth--;
    }

    private void SpawnReward(Vector2 position)
    {
        _gemSpawner.SpawnReward(position);
    }
}
