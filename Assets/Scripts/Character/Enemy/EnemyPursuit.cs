using UnityEngine;

public class EnemyPursuit : MonoBehaviour
{
    [SerializeField] private float _pursuingDistance = 6f;
    [SerializeField] private float _pursuingVerticalDistance = 1f;
    [SerializeField] private GameObject _pursuitSign;
    [SerializeField] private Character _character;

    public float Destination => _character.transform.position.x;

    public bool CharacterIsNear()
    {
        if(Mathf.Abs(_character.transform.position.y - transform.position.y) <= _pursuingVerticalDistance
            && transform.position.IsEnoughClose(_character.transform.position, _pursuingDistance)
            && _character.IsAvailable())
        {
            _pursuitSign.SetActive(true);
            return true;
        }

        _pursuitSign.SetActive(false);
        return false;
    }
}
