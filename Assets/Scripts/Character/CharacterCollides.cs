using System;
using UnityEngine;

public class CharacterCollides : MonoBehaviour
{
    public event Action Damaged;
    public event Action GemCollected;
    public event Action JumpEnemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Gem>() != null)
            GemCollected?.Invoke();

        if (other.gameObject.GetComponent<EnemyBody>() != null)
            Damaged?.Invoke();

        if (other.gameObject.GetComponent<EnemyWeakPlace>() != null)
            JumpEnemy?.Invoke();
    }
}
