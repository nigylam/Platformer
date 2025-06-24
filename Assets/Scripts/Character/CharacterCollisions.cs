using System;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    public event Action Damaged;
    public event Action GemCollected;
    public event Action HealingCollected;
    public event Action JumpEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Gem gem))
            GemCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out Healing healing))
            HealingCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out EnemyBody enemyBody))
            Damaged?.Invoke();

        if (other.gameObject.TryGetComponent(out EnemyWeakPlace enemyWeak))
            JumpEnemy?.Invoke();
    }
}
