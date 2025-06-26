using System.Collections;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private readonly int DamagedState = Animator.StringToHash("Damaged");
    private readonly int IdleState = Animator.StringToHash("Idle");
    private readonly int FloatVelocityY = Animator.StringToHash("VelocityY");
    private readonly int FloatVelocityX = Animator.StringToHash("VelocityX");
    private readonly int BoolIsGrounded = Animator.StringToHash("IsGrounded");
    private readonly int BoolIsStuned = Animator.StringToHash("IsStuned");

    [SerializeField] private float _damageAnimationDuration = 2f;

    private WaitForSeconds _damageAnimationDurationWait;

    private Character _character;
    private Animator _animator;
    private Coroutine _setDamagedStop;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
        _damageAnimationDurationWait = new WaitForSeconds(_damageAnimationDuration);
    }

    private void Update()
    {
        _animator.SetBool(BoolIsGrounded, _character.GroundChecker.IsGrounded());
        _animator.SetBool(BoolIsStuned, _character.Collisions.IsStuned);
        _animator.SetFloat(FloatVelocityY, _character.Movement.RigidbodyVelocityY);
        _animator.SetFloat(FloatVelocityX, Mathf.Abs(_character.Movement.RigidbodyVelocityX));
    }

    private void OnEnable()
    {
        _character.Collisions.Damaged += SetDamaged;
        _character.Dead += SetDead;
        _character.Respawned += SetRespawned;
    }

    private void OnDisable()
    {
        _character.Collisions.Damaged -= SetDamaged;
        _character.Dead -= SetDead;
        _character.Respawned -= SetRespawned;
    }

    private void SetDamaged()
    {
        _animator.Play(DamagedState);
        _setDamagedStop = StartCoroutine(DisableDamageAnimation());
    }

    private void SetDead()
    {
        _animator.Play(DamagedState);
        StopCoroutine(_setDamagedStop);
    }

    private void SetRespawned() => _animator.Play(IdleState);

    private IEnumerator DisableDamageAnimation()
    {
        yield return _damageAnimationDurationWait;
        _animator.Play(IdleState);
    }
}
