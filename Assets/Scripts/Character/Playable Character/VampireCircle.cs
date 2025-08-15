using System;
using UnityEngine;

public class VampireCircle : MonoBehaviour
{
    public event Action<Enemy> EnemyEntered;
    public event Action<Enemy> EnemyExited;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyExited?.Invoke(enemy);
        }
    }
}
