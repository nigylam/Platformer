using System;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public event Action Restarted;
    public event Action Won;

    [SerializeField] private Character _character;
    [SerializeField] private CharacterCollides _characterCollides;
    [SerializeField] private Transform _gameEnd;
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private TextMeshProUGUI _gemsCounter;
    [SerializeField] private Gem[] _gemsFree;
    [SerializeField] private Gem[] _gemsEnemy;
    [SerializeField] private int _gemsMax;
    [SerializeField] private int _currentGems = 0;

    private void Start()
    {
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
        _characterCollides.GemCollected += CollectGem;
    }

    private void OnDisable()
    {
        _character.Dead -= GameOver;
        _characterCollides.GemCollected -= CollectGem;
    }

    private void Win()
    {
        _gameEnd.gameObject.SetActive(true);
        _gameEndText.text = "You win!";
        _gameEndText.color = Color.green;
        Won?.Invoke();
    }

    private void GameOver(bool isDead)
    {
        if (isDead)
        {
            _gameEnd.gameObject.SetActive(true);
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

        foreach (Gem gem in _gemsEnemy)
            gem.gameObject.SetActive(false);

        _currentGems = 0;
        _gameEnd.gameObject.SetActive(false);

        Restarted?.Invoke();
    }


}
