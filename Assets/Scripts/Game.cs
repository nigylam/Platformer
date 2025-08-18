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
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CollectableSpawner _gemSpawner;
    [SerializeField] private CollectableSpawner _medkitSpawner;

    public int GemsMax => _gemsMax;

    private int _gemsMax;
    private int _gemsCount;

    public int GemsCount
    {
        get
        {
            return _gemsCount;
        }
        private set
        {
            _gemsCount = value;

            if (_gemsCount >= _gemsMax)
            {
                End(true);
            }
        }
    }

    public event Action Won;
    public event Action Over;
    public event Action<int> GemsCountChanged;
    public event Action Restarted;

    private void Start()
    {
        Restart();
    }

    private void OnEnable()
    {
        _character.Collisions.GemCollided += CollectGem;
        _character.Healed += CollectHealing;
        _enemySpawner.Dead += SpawnReward;
        _character.Dead += End;
    }

    private void OnDisable()
    {
        _character.Collisions.GemCollided -= CollectGem;
        _character.Healed -= CollectHealing;
        _enemySpawner.Dead -= SpawnReward;
        _character.Dead -= End;
    }

    public void Restart()
    {
        _gemSpawner.Despawn();
        _gemSpawner.Spawn();
        _gemsMax = _gemSpawner.Count + _enemySpawner.Count;
        GemsCount = 0;
        _medkitSpawner.Despawn();
        _medkitSpawner.Spawn();
        _character.Respawn(_characterStartHealth, _characterMaxHealth);
        _enemySpawner.Respawn();
        Restarted?.Invoke();
    }

    private void End()
    {
        End(false);
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
            Over?.Invoke();
        }
    }

    private void CollectGem(Gem gem)
    {
        gem.Collect();
        GemsCount++;
        GemsCountChanged?.Invoke(GemsCount);
        _gemSpawner.Sounds.Play();
    }

    private void CollectHealing()
    {
        _medkitSpawner.Sounds.Play();
    }

    private void SpawnReward(Vector2 position)
    {
        _gemSpawner.SpawnReward(position);
    }
}
