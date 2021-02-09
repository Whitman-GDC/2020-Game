using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private Rigidbody rb;

    public float moveSpeed = 12f;
    public float stealthSpeedMultiplier = 0.5f;

    public float jumpForce = 7f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private new BoxCollider collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
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

        if (newMove.x != 0 || newMove.z != 0)
		{
            myAnimationController.SetBool("Running", true);
		} else
		{
            myAnimationController.SetBool("Running", false);
        }

        rb.velocity = newMove;

        //jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private bool IsGrounded()
	{
        return Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x,
            collider.bounds.min.y, collider.bounds.center.z), collider.size.z * 0.9f, groundMask);
	}

    public void CaughtInWeb(float multiplier)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * multiplier, rb.velocity.z);
        moveSpeed *= multiplier;
    }

    public void LeaveWeb(float multiplier)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 1 / multiplier, rb.velocity.z);
        moveSpeed *= 1 / multiplier;
	}

}
