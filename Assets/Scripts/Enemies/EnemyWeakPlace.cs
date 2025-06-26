using UnityEngine;

public class EnemyWeakPlace : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character _))
        {
            _enemy.Die();
        }
    }
}
