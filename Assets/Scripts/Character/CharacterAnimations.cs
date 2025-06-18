using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private readonly int DamagedState = Animator.StringToHash("Damaged");
    private readonly int IdleState = Animator.StringToHash("Idle");
    private readonly int FloatVelocityY = Animator.StringToHash("VelocityY");
    private readonly int FloatVelocityX = Animator.StringToHash("VelocityX");
    private readonly int BoolIsGrounded = Animator.StringToHash("IsGrounded");

    [SerializeField] private CharacterMovement _controller;
    [SerializeField] private CharacterCollisions _collides;
    [SerializeField] private Character _character;
    [SerializeField] private Animator Animator;

    private void Update()
    {
        Animator.SetBool(BoolIsGrounded, _controller.IsGrounded());
        Animator.SetFloat(FloatVelocityY, _controller.RigidbodyVelocityY);
        Animator.SetFloat(FloatVelocityX, Mathf.Abs(_controller.RigidbodyVelocityX));
    }

    private void OnEnable()
    {
        _collides.Damaged += SetDamaged;
        _character.Respawned += SetRespawned;
    }

    private void OnDisable()
    {
        _collides.Damaged -= SetDamaged;
        _character.Respawned -= SetRespawned;
    }

    private void SetDamaged()
    {
        Animator.Play(DamagedState);
    }

    private void SetRespawned() => Animator.Play(IdleState);
}
