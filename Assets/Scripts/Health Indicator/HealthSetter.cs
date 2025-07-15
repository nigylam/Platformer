using UnityEngine;

public class HealthSetter : MonoBehaviour
{
    [SerializeField] private int _start = 100;
    [SerializeField] private int _max = 100;
    [SerializeField] private int _heal = 10;
    [SerializeField] private int _damage = 20;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Set(_start, _max);
    }

    public void TakeDamage()
    {
        _health.Decrease(_damage);
    }

    public void Heal()
    {
        _health.CanIncrease(_heal);
    }
}
