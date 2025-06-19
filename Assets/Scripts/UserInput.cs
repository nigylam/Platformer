using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public event Action JumpKeyPressed; 

    public float HorizontalInput {  get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpKeyPressed?.Invoke();
        }
    }
}
