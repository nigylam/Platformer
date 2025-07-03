using System.Collections;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private readonly int DamagedState = Animator.StringToHash("Damaged");
    private readonly int IdleState = Animator.StringToHash("Idle");
    private readonly int FloatVelocityY = Animator.StringToHash("VelocityY");
    private readonly int FloatVelocityX = Animator.StringToHash("VelocityX");
    private readonly int BoolIsGrounded = Animator.StringToHash("IsGrounded");
    private readonly int BoolIsStuned = Animator.StringToHash("IsStuned");

    [SerializeField] private float _damageAnimationDuration = 2f;

    private WaitForSeconds _damageAnimationDurationWait;

    private Animator _animator;
    private Coroutine _setDamagedStop;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damageAnimationDurationWait = new WaitForSeconds(_damageAnimationDuration);
    }

    public void SetTriggers(bool isGrounded, bool isStuned, float velocityY, float velocityX)
    {
        _animator.SetBool(BoolIsGrounded, isGrounded);
        _animator.SetBool(BoolIsStuned, isStuned);
        _animator.SetFloat(FloatVelocityY, velocityY);
        _animator.SetFloat(FloatVelocityX, Mathf.Abs(velocityX));
    }

    public void SetDamaged()
    {
        _animator.Play(DamagedState);
        _setDamagedStop = StartCoroutine(DisableDamageAnimation());
    }

    public void SetDead()
    {
        _animator.Play(DamagedState);
        StopCoroutine(_setDamagedStop);
    }

    public void SetRespawned() => _animator.Play(IdleState);

    private IEnumerator DisableDamageAnimation()
    {
        yield return _damageAnimationDurationWait;
        _animator.Play(IdleState);
    }
}
