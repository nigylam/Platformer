using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Collectable : MonoBehaviour
{

    private readonly int DisapearAnimation = Animator.StringToHash("Disapear");

    [SerializeField] private int _power = 1;

    public int Power => _power;

    private Animator _animator;
    private Collider2D _triggerCollider;


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
        _animator.Play(DisapearAnimation);
        _triggerCollider.enabled = false;
    }

    public void OnDisapearAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
