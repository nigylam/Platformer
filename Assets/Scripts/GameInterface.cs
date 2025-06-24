using TMPro;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    [SerializeField] private GameObject _gameEnd;
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private TextMeshProUGUI _gemsCounter;

    private Game _game;

    private void Awake()
    {
        _game = GetComponent<Game>();
    }

    private void OnEnable()
    {
        _game.GemsCountChanged += ChangeGemCounter;
        _game.Restarted += DisableEndScreen;
        _game.Won += Win;
        _game.Over += Over;
    }

    private void OnDisable()
    {
        _game.GemsCountChanged -= ChangeGemCounter;
        _game.Restarted -= DisableEndScreen;
        _game.Won -= Win;
        _game.Over -= Over;
    }

    private void ChangeGemCounter(int count)
    {
        _gemsCounter.text = $"{count}/{_game.GemsMax}";
    }

    private void DisableEndScreen()
    {
        _gameEnd.SetActive(false);
        ChangeGemCounter(0);
    }

    private void Win()
    {
        EnableEndScreen(true);
    }

    private void Over()
    {
        EnableEndScreen(false);
    }

    private void EnableEndScreen(bool isWin)
    {
        _gameEnd.SetActive(true);

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
}
