using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int DieAnimation = Animator.StringToHash("Die");

    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _weekPlaceCollider;
    [SerializeField] private Collectable _gemReward;
    [SerializeField] private AudioSource _dieSound;
    [SerializeField] private AnimationClip _dieClip;

    public event Action Dead;
    public event Action Respawned;

    public float JumpPadForce { get; private set; } = 3f;

    private WaitForSeconds _dieAnimationDelay;

    private void Awake()
    {
        _dieAnimationDelay = new WaitForSeconds(_dieClip.length);
    }

    public void Die()
    {
        _bodyCollider.enabled = false;
        _dieSound.Play();
        SpawnReward();
        _animator.Play(DieAnimation);
        StartCoroutine(DisableEnemySpriteAfterDelay());
        Dead?.Invoke();
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _animator.enabled = true;
        _bodyCollider.enabled = true;
        _weekPlaceCollider.enabled = true;
        Respawned?.Invoke();
    }

    private void SpawnReward()
    {
        _gemReward.gameObject.SetActive(true);
        _gemReward.transform.position = transform.position;
    }

    private IEnumerator DisableEnemySpriteAfterDelay()
    {
        yield return _dieAnimationDelay;
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
}
