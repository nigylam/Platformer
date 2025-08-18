using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Collectable : MonoBehaviour
{
    private readonly int _disapearAnimation = Animator.StringToHash("Disapear");

    [SerializeField] private int _power = 1;

    private Animator _animator;
    private Collider2D _triggerCollider;

    public int Power => _power;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _triggerCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _triggerCollider.enabled = true;
    }

    public void Collect()
    {
        _animator.Play(_disapearAnimation);
        _triggerCollider.enabled = false;
    }

    public void OnDisapearAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
