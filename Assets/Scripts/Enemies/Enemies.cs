using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public int Count => _enemies.Length;

    public void Respawn()
    {
        foreach(Enemy enemy in _enemies) 
            enemy.Respawn(); 
    }
}
