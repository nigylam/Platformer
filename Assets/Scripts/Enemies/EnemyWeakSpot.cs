using System;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public event Action Damaged;

    public void GetDamage()
    {
        Damaged?.Invoke();
    }
}
