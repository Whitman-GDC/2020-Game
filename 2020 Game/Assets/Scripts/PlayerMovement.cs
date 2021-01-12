using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public CharacterController controller;

    public float moveSpeed = 12f;
    public float stealthSpeedMultiplier = 0.5f;

    public float gravity = -9.81f;
    public float jumpForce;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        /*
        if (isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}
        */
        
        //input
        float movementX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float movementZ = Input.GetAxisRaw("Vertical") * moveSpeed;

        //movement
        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        Vector3 newMove = new Vector3(move.x, rb.velocity.y, move.z);

        rb.velocity = newMove;
        
        if(isGrounded)
        {
            Console.WriteLine("is grounded");
        }
        //jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        /*
        float finalMoveSpeed = moveSpeed;

        if (Input.GetButton("Stealth"))
		{
            finalMoveSpeed *= stealthSpeedMultiplier;
		}
		else
		{
            finalMoveSpeed = moveSpeed;
		}

        controller.Move(move * finalMoveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        */
    }
}
