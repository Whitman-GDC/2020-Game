using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jumping : MonoBehaviour
{
    public Transform player;
    public Vector3 destination;
    bool IsDistance;
    Rigidbody rb;
    public float JumpingForce;
    bool ready;

    IEnumerator WaitAndDoSomething2()
    {
        rb.AddForce(Vector3.up * JumpingForce * 0.4f);
        yield return new WaitForSeconds(0.1f);
        ready = false;
        yield return new WaitForSeconds(3.0f);
        ready = true;

    }

    IEnumerator WaitAndDoSomething()
    {

            rb.AddForce(Vector3.up * JumpingForce * 2.5f);
        transform.Rotate(0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        rb.AddRelativeForce(Vector3.forward * JumpingForce * 5.0f);
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);
        yield return new WaitForSeconds(0.1f);
        transform.Rotate(0, 0, -1);
        ready = false;
        yield return new WaitForSeconds(3.0f);
        ready = true;

    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ready = true;


    }


    private void OnTriggerEnter(Collider other)
    {
        IsDistance = true;

    }
    private void OnTriggerExit(Collider other)
    {
        IsDistance = false;

    }

    private void Update()
    {
        destination = transform.position;
        destination.y = transform.position.y + 20.0f;

        if (IsDistance == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (ready == true)
                {
                    StartCoroutine(WaitAndDoSomething());
                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (ready == true)
                {

                    StartCoroutine(WaitAndDoSomething2());

                }
            }
        }
    }

}






