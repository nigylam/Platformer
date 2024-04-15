using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int DieAnimation = Animator.StringToHash("Die");

    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _weekPlaceCollider;
    [SerializeField] private Gem _gemReward;
    [SerializeField] private AudioSource _dieSound;

    public event Action Dead;
    public event Action Respawned;

    public float JumpPadForce { get; private set; } = 3f;

    public void Die()
    {
        _bodyCollider.enabled = false;
        _dieSound.Play();
        SpawnReward();
        _animator.Play(DieAnimation);
        Dead?.Invoke();
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        _bodyCollider.enabled = true;
        _weekPlaceCollider.enabled = true;
        Respawned?.Invoke();
    }

    private void SpawnReward()
    {
        _gemReward.gameObject.SetActive(true);
        _gemReward.transform.position = transform.position;
    }
}
