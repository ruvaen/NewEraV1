using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator playerAnimator;
    /*
    public void HandleMovement(MovementState _movementState, float _lookToForwardAngle, Vector3 _movementDirection)
    {
        switch (_movementState)
        {
            case MovementState.Running:
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.SetBool("isWalking", false);
                break;
            case MovementState.Walking:
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isWalking", true);
                playerAnimator.SetFloat("LookToForwardAngle", _lookToForwardAngle);
                break;
            case MovementState.Idle:
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isWalking", false);
                break;
        }
    }
    */
    public void HandleMovement(Transform _playerTransform, Vector3 _moveDirection, MovementState _movementState)
    {
        playerAnimator.SetFloat("MoveHorizontal", Vector3.Dot(_playerTransform.right.normalized, _moveDirection), 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("MoveVertical", (_movementState == MovementState.Running) ? 2f : Vector3.Dot(_playerTransform.forward.normalized, _moveDirection), 0.1f, Time.deltaTime);
    }
       
}
