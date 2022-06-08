using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 6f;

    private void FixedUpdate()
    {
        HandleJoystickMovement();
    }

    void HandleJoystickMovement()
    {
        float horizontal = joystick.Vertical;
        float vertical = joystick.Horizontal;
        Vector3 direction = new Vector3(-horizontal, 0f, vertical).normalized;

        controller.SimpleMove(direction * moveSpeed * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Running", true);
            float targetAngle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    


}
