using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float radius = 0.2f;

    public bool IsGrounded() => Physics2D.OverlapCircle(transform.position, radius, _groundLayer);
}
