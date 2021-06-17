using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
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
}
