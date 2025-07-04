using UnityEngine;

public class Fliper : MonoBehaviour
{
    [SerializeField] private bool _isFacingRight;

    private float _horizontalMoving;

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
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
