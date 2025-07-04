using UnityEngine;

public class EnemyMovement : Movement
{
    public override void Move(float horizontalMovement)
    {
        base.Move(horizontalMovement);

        transform.Translate(horizontalMovement * Speed * Time.deltaTime, 0f, 0f);
    }
}
