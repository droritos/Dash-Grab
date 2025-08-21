using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    public void HandleMovement(float movementMagnitude)
    {
        animator.SetFloat(SpeedHash, movementMagnitude);
    }
}
