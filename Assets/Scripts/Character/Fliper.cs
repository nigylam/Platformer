using UnityEngine;

public class Fliper : MonoBehaviour
{
    [SerializeField] private bool _isFacingRight;

    private SpriteRenderer _spriteRenderer;
    private float _horizontalMoving;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
    }

    public void SetHorizontalMoving(float horizontalMoving)
    {
        _horizontalMoving = horizontalMoving;
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontalMoving < 0 || _isFacingRight == false && _horizontalMoving > 0)
        {
            _isFacingRight = !_isFacingRight;
            bool isFliped = _spriteRenderer.flipX;
            _spriteRenderer.flipX = !isFliped;
        }
    }
}
