using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    //public float speed = 30.0F;
    //
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float rotateSpeed = 3.0F;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            characterController.SimpleMove(forward * curSpeed);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
//void Update()
//{
//    CharacterController controller = GetComponent<CharacterController>();

//    // Rotate around y - axis
//    transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

//    // Move forward / backward
//    Vector3 forward = transform.TransformDirection(Vector3.forward);
//    float curSpeed = speed * Input.GetAxis("Vertical");
//    controller.SimpleMove(forward * curSpeed);
//}
//}