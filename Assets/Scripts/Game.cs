using System;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterCollisions _characterCollisions;
    [SerializeField] private GameObject _gameEnd;
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private TextMeshProUGUI _gemsCounter;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private EnemiesRespawner _enemiesRespawner;
    [SerializeField] private Gem[] _gemsFree;
    [SerializeField] private Gem[] _gemsEnemy;

    private int _gemsMax;
    private int _currentGems;

    public event Action Won;

    private void Start()
    {
        _currentGems = 0;
        _gemsMax = _gemsFree.Length + _gemsEnemy.Length;
        HideRewardGems();
    }

    private void Update()
    {
        _gemsCounter.text = $"{_currentGems}/{_gemsMax}";

        if (_currentGems == _gemsMax)
            Win();
    }

    private void OnEnable()
    {
        _character.Dead += GameOver;
        _characterCollisions.GemCollected += CollectGem;
    }

    private void OnDisable()
    {
        _character.Dead -= GameOver;
        _characterCollisions.GemCollected -= CollectGem;
    }

    private void Win()
    {
        GameEnd(true);
        Won?.Invoke();
    }

    private void GameOver()
    {
        GameEnd(false);
    }

    private void GameEnd(bool isWin)
    {
        _gameEnd.SetActive(true);
        _characterMovement.SetDisable();

        if (isWin)
        {
            _gameEndText.text = "You win!";
            _gameEndText.color = Color.green;
        }
        else
        {
            _gameEndText.text = "Game over!";
            _gameEndText.color = Color.red;
        }
    }

    private void HideRewardGems()
    {
        foreach (Gem gem in _gemsEnemy)
            gem.gameObject.SetActive(false);
    }

    public void CollectGem()
    {
        _currentGems++;
    }

    public void Restart()
    {
        foreach (Gem gem in _gemsFree)
            gem.gameObject.SetActive(true);

        HideRewardGems();

        _currentGems = 0;
        _gameEnd.SetActive(false);
        _character.Respawn();
        _enemiesRespawner.Respawn();
    }
}
