using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float moveSpeed = 12f;
    public float stealthSpeedMultiplier = 0.5f;

    public float jumpForce = 7f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private new CapsuleCollider collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float finalMoveSpeed = moveSpeed;

        if (Input.GetButton("Stealth"))
        {
            finalMoveSpeed *= stealthSpeedMultiplier;
        }
        else
        {
            finalMoveSpeed = moveSpeed;
        }

        //input
        float movementX = Input.GetAxisRaw("Horizontal") * finalMoveSpeed;
        float movementZ = Input.GetAxisRaw("Vertical") * finalMoveSpeed;

        //movement
        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        Vector3 newMove = new Vector3(move.x, rb.velocity.y, move.z);

        if (Input.GetKeyDown(KeyCode.Space))
		{
            Debug.Log("Space bar clicked");
		}

        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        rb.velocity = newMove;
    }

    private bool IsGrounded()
	{
        return Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x,
            collider.bounds.min.y, collider.bounds.center.z), collider.radius * 0.9f, groundMask);
	}
}
