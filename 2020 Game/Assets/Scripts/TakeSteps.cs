using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TakeSteps : MonoBehaviour
{
    public Transform target;
    public float stepLength;
    public float speed = 1;
    public float marginOfError;
    private Transform transform;
    private Vector3 nextTarget;  
    private bool takingStep = false;
    

    void Start()
    {
        transform = GetComponent<Transform>();
        nextTarget = transform.position;
    }

    void Update()
    {
        //This is the best way I found to smoothly move the base of the leg to the target. Transform.Translate is too fast.
        if((Math.Abs((target.position-transform.position).magnitude) >= stepLength) && !takingStep) {
            takingStep = true; 
            nextTarget = target.position;
        }

        if(takingStep) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if(takingStep && Math.Abs((transform.position-nextTarget).magnitude) <=marginOfError)
        {
            takingStep = false;
        }

        Debug.Log(takingStep);
    }
}
