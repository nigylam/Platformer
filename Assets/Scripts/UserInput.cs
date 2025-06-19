using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public event Action JumpKeyPressed; 

    public float HorizontalRaw {  get; private set; }

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpKeyPressed?.Invoke();
        }
    }
}
