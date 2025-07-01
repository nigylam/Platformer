using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    public void Attack(EnemyWeakSpot enemy)
    {
        enemy.GetDamage();
    }
}
