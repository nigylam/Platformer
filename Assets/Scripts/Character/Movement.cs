using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class Movement : MonoBehaviour
{
    protected float Speed;

    private Fliper _fliper;
    private Vector2 _startPosition;

    private void Awake()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public virtual void Move(float horizontalMovement)
    {
        _fliper.SetHorizontalMoving(horizontalMovement);
    }

    public void ChangeSpeed(float speed)
    {
        Speed = speed;
    }

    public void Reset()
    {
        transform.position = _startPosition;
    }
}
