using System;
using System.Collections;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    private readonly string NormalLayer = "Player";
    private readonly string StunedLayer = "PlayerStuned";

    [SerializeField] private float _stunedTime = 2f;

    public bool IsStuned => _isStuned;

    public event Action<Enemy, float> EnemyCollided;
    public event Action<Gem> GemCollided;
    public event Action<Medkit> MedkitCollided;

    private bool _isStuned = false;
    private WaitForSeconds _stunedTimeWait;
    private Coroutine _stunStop;

    private void Awake()
    {
        _stunedTimeWait = new WaitForSeconds(_stunedTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Gem gem))
            GemCollided?.Invoke(gem);

        if (other.gameObject.TryGetComponent(out Medkit healing))
            MedkitCollided?.Invoke(healing);

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            float height = transform.position.y - enemy.transform.position.y;
            Debug.Log(height);  
            EnemyCollided?.Invoke(enemy, height);
        }
    }

    public void CancelStun()
    {
        if (_isStuned)
        {
            Unstan();
            StopCoroutine(_stunStop);
        }
    }

    public void Stun()
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
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(StunedLayer);
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
