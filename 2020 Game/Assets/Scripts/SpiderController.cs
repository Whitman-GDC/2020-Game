using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiderController : MonoBehaviour
{
    //front
    //7--6
    //5--4
    //3--2
    //1--0
    //back
    public StepManager[] legs;

    public float speed;
    private Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();

        //Offset legs 0, 7, 3, 4 forward and the other 4 legs backward by half of the step length
        for(int i = 0; i<legs.Length; i++)
        {
            if(i == 0 || i == 7 || i == 3 || i == 4)
            {
                //Vectors occur
                legs[i].setPosition(legs[i].getPosition() + new Vector3(transform.forward.x/2, 0, transform.forward.z/2));
            }
            else
            {
                legs[i].setPosition(legs[i].getPosition() + new Vector3(-transform.forward.x/2, 0, -transform.forward.z/2));
            }
        }
    }

    void FixedUpdate()
    {
        //Move for now just straight forward. To make the spider follow more complex paths just change how the transform is moved right here and the legs should follow
        transform.position += transform.forward*speed;

        for(int i = 0; i<legs.Length; i++)
        {
            if((Math.Abs((legs[i].getTargetPosition()-legs[i].getPosition()).magnitude) >= legs[i].stepLength) && !legs[i].getTakingStep()) {
                legs[i].StartStep();
            }
        }
    }
}
