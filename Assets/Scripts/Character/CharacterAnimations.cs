using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private readonly int DamagedState = Animator.StringToHash("Damaged");
    private readonly int IdleState = Animator.StringToHash("Idle");
    private readonly int FloatVelocityY = Animator.StringToHash("VelocityY");
    private readonly int FloatVelocityX = Animator.StringToHash("VelocityX");
    private readonly int BoolIsGrounded = Animator.StringToHash("IsGrounded");

    [SerializeField] private Animator Animator;

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        Animator.SetBool(BoolIsGrounded, _character.GroundChecker.IsGrounded());
        Animator.SetFloat(FloatVelocityY, _character.Movement.RigidbodyVelocityY);
        Animator.SetFloat(FloatVelocityX, Mathf.Abs(_character.Movement.RigidbodyVelocityX));
    }

    private void OnEnable()
    {
        _character.Collisions.Damaged += SetDamaged;
        _character.Respawned += SetRespawned;
    }

    private void OnDisable()
    {
        _character.Collisions.Damaged -= SetDamaged;
        _character.Respawned -= SetRespawned;
    }

    private void SetDamaged()
    {
        Animator.Play(DamagedState);
    }

    private void SetRespawned() => Animator.Play(IdleState);
}
