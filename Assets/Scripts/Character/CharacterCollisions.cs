using System;
using System.Collections;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    private readonly string NormalLayer = "Player";
    private readonly string StunedLayer = "PlayerStuned";

    [SerializeField] private float _stunedTime = 2f;

    public bool IsStuned => _isStuned;

    public event Action Damaged;
    public event Action GemCollected;
    public event Action HealingCollected;
    public event Action JumpEnemy;

    private bool _isStuned;
    private WaitForSeconds _stunedTimeWait;
    private Coroutine _stunStop;
    private GroundChecker _groundChecker;

    private void Awake()
    {
        _stunedTimeWait = new WaitForSeconds(_stunedTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Gem _))
            GemCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out Healing _))
            HealingCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out EnemyBody _))
        {
            GetStuned();
            Damaged?.Invoke();
        }

        if (other.gameObject.TryGetComponent(out EnemyWeakPlace _))
        {
            JumpEnemy?.Invoke();
        }
    }

    public void SetGroundChecker(GroundChecker groundChecker)
    {
        _groundChecker = groundChecker;
    }

    public void CancelStun()
    {
        if (_isStuned)
        {
            Unstan();
            StopCoroutine(_stunStop);
        }
    }

    private void GetStuned()
    {
        _isStuned = true;
        gameObject.layer = LayerMask.NameToLayer(StunedLayer);
        _groundChecker.gameObject.layer = LayerMask.NameToLayer(StunedLayer);
        _stunStop = StartCoroutine(DisableStuned());
    }

    private void Unstan()
    {
        _isStuned = false;
        gameObject.layer = LayerMask.NameToLayer(NormalLayer);
        _groundChecker.gameObject.layer = LayerMask.NameToLayer(NormalLayer);
    }

    private IEnumerator DisableStuned()
    {
        yield return _stunedTimeWait;
        Unstan();
    }
}
