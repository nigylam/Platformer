using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public event Action ReadyForDisable;

    public void OnAnimationEnding()
    {
        ReadyForDisable?.Invoke();
    }
}
