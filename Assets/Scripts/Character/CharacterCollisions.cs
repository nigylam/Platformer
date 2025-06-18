using System;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    public event Action Damaged;
    public event Action GemCollected;
    public event Action JumpEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Gem>(out Gem gem))
            GemCollected?.Invoke();

        if (other.gameObject.TryGetComponent<EnemyBody>(out EnemyBody enemyBody))
            Damaged?.Invoke();

        if (other.gameObject.TryGetComponent<EnemyWeakPlace>(out EnemyWeakPlace enemyWeak))
            JumpEnemy?.Invoke();
    }
}
