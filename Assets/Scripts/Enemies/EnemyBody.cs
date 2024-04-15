using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Collider2D _weekPlaceCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
            _weekPlaceCollider.enabled = false;
    }
}
