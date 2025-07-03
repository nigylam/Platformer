using System;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public event Action<int> Damaged;

    public void GetDamage(int damage = 1)
    {
        Damaged?.Invoke(damage);
    }
}
