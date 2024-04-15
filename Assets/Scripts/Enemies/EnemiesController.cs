using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Game _game;

    private void OnEnable()
    {
        _game.Restarted += Respawn;
    }

    private void OnDisable()
    {
        _game.Restarted -= Respawn;
    }

    private void Respawn()
    {
        foreach(Enemy enemy in _enemies) 
            enemy.Respawn(); 
    }
}
