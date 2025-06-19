using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterFlip : MonoBehaviour
{
    private Character _character;

    private bool _isFacingRight = true;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (_isFacingRight && _character.UserInput.HorizontalRaw < 0 || _isFacingRight == false && _character.UserInput.HorizontalRaw > 0)
        {
            _isFacingRight = !_isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
