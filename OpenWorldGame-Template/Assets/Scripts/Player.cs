using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;

    private CharacterController controller; // Reference to CharacterController
    private Vector3 motion; // Is the movement offset per frame

    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundRayDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get W, A, S, D, left, right, up, down Input
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // Move character motion with inputs 
        Move(inputH, inputV);
        // Is the player grounded
        if(IsGrounded())
        {
            // Cancel gravity
            motion.y = 0f;
            // Pressing jump button
            if(Input.GetButtonDown("Jump"))
            {
                // Make the player jump
                motion.y = jumpHeight;
            }
        }
        // Apply gravity
        motion.y += gravity * Time.deltaTime;
        // Move the controller with motion
        controller.Move(motion * Time.deltaTime);
        
    }

    // Check if the player is touching the ground
    bool IsGrounded()
    {
        return controller.isGrounded;
        // Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        // If hitting something
        return Physics.Raycast(groundRay, groundRayDistance);

    }

    // Move the character's motion in direction of unput 
    void Move(float inputH, float inputV)
    {
        // Generate direction from input
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        // Contvert local space to world space direction
        direction = transform.TransformDirection(direction);

        // Apply motion to only x and z
        motion.x = direction.x * walkSpeed;
        motion.z = direction.z * walkSpeed;
    }
    
}
