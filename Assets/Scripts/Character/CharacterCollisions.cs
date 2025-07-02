using System;
using System.Collections;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    private readonly string NormalLayer = "Player";
    private readonly string StunedLayer = "PlayerStuned";

    [SerializeField] private float _stunedTime = 2f;

    public bool IsStuned => _isStuned;

    public event Action EnemyCollided;
    public event Action<Gem> GemCollided;
    public event Action<Healing> HealingCollided;
    public event Action<EnemyWeakSpot> EnemyWeakSpotCollided;

    private bool _isStuned = false;
    private WaitForSeconds _stunedTimeWait;
    private Coroutine _stunStop;
    private GroundChecker _groundChecker;

    private void Awake()
    {
        _stunedTimeWait = new WaitForSeconds(_stunedTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Gem gem))
            GemCollided?.Invoke(gem);

        if (other.gameObject.TryGetComponent(out Healing healing))
            HealingCollided?.Invoke(healing);

        if (other.gameObject.TryGetComponent(out EnemyBody _))
        {
            EnemyCollided?.Invoke();
        }

        if (other.gameObject.TryGetComponent(out EnemyWeakSpot enemy))
        {
            EnemyWeakSpotCollided?.Invoke(enemy);
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

    public void GetStuned()
    {
        ChangeLayer(true);
        _stunStop = StartCoroutine(DisableStuned());
    }

    public void ChangeLayer(bool isStuned)
    {
        _isStuned = isStuned;

        if (_isStuned == false)
        {
            gameObject.layer = LayerMask.NameToLayer(NormalLayer);
            _groundChecker.gameObject.layer = LayerMask.NameToLayer(NormalLayer);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(StunedLayer);
            _groundChecker.gameObject.layer = LayerMask.NameToLayer(StunedLayer);
        }
    }

    private void Unstan()
    {
        ChangeLayer(false);
    }

    private IEnumerator DisableStuned()
    {
        yield return _stunedTimeWait;
        Unstan();
    }
}
