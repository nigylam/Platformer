using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public event Action ReadyForDisable;

    public int Damage { get; private set; }

    public void Set(int damage) => Damage = damage;

    public void OnAnimationEnding()
    {
        ReadyForDisable?.Invoke();
    }
}
