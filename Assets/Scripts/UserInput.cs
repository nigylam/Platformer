using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    private KeyCode _jump = KeyCode.W;
    private KeyCode _ability = KeyCode.E;

    public event Action JumpKeyPressed;
    public event Action AbilityKeyPressed;

    public float HorizontalRaw { get; private set; }

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw(Horizontal);

        if (Input.GetKeyDown(_jump))
        {
            JumpKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_ability))
            AbilityKeyPressed?.Invoke();
    }
}
