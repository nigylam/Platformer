using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage();
    }
}
