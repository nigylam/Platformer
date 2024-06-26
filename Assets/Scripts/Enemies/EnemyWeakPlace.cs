using System;
using UnityEngine;

public class EnemyWeakPlace : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public event Action Dead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControler>() != null)
        {
            _enemy.Die();
            Dead?.Invoke();
        }
    }
}
