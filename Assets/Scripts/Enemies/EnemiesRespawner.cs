using UnityEngine;

public class EnemiesRespawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public void Respawn()
    {
        foreach(Enemy enemy in _enemies) 
            enemy.Respawn(); 
    }
}
