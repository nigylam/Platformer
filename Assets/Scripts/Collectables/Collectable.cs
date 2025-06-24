using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Collectable : MonoBehaviour
{
    private readonly int DisapearAnimation = Animator.StringToHash("Disapear");

    private Animator _animator;
    private Collider2D _triggerCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _triggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Character>() != null)
        {
            _animator.Play(DisapearAnimation);
            _triggerCollider.enabled = false;
        }
    }

    private void OnEnable()
    {
        _triggerCollider.enabled = true;
    }

    public void OnDisapearAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
