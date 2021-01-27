using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
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
        transform.position = target.position;
    }

    void Update()
    {
        //This is the best way I found to smoothly move the base of the leg to the target. Transform.Translate is too fast.
        if(takingStep) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextTarget, step);
        }

        if(takingStep && Math.Abs((transform.position-nextTarget).magnitude) <=marginOfError)
        {
            takingStep = false;
        }
    }

    //Move the base of the leg to the target
    public void StartStep()
    {
        if(!takingStep)
        {
            takingStep = true;
            nextTarget = target.position;
        }
    }
    
    public Vector3 getPosition()
    {
        return transform.position;
    }

    public void setPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public Vector3 getTargetPosition()
    {
        return target.position;
    }

    public bool getTakingStep()
    {
        return takingStep;
    }
}
