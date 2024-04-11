using UnityEngine;

public class Restarter : MonoBehaviour
{
    [SerializeField] private Game _game;

    public void OnClick()
    {
        _game.Restart();
    }
}
