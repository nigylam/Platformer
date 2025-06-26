using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void OnAnimationEnding()
    {
        _enemy.gameObject.SetActive(false);
    }
}
