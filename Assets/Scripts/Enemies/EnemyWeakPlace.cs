using System;
using UnityEngine;

public class EnemyWeakPlace : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public event Action Dead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterMovement _))
        {
            _enemy.Die();
            Dead?.Invoke();
        }

        Vector2 vector = new Vector2(1, 0);
    }
}
