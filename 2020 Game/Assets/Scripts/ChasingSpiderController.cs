using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-1)] //Make sure giving it input comes before the spider object's calculations
public class ChasingSpiderController : MonoBehaviour{ 
    public Spider spider;
    public Transform target;
    public float error = 5f;
    
    void FixedUpdate() {
        //find the vector that the spider needs to be facing along to approach the target, turn to it if the spider is not already facing it, then move forward
        //this will not be the shortest possible path to the target if the target is not on the same plane the spider is standing on, but it should be close enough
        Vector3 targetFacing = Vector3.ProjectOnPlane(Vector3.Normalize(target.transform.position-spider.transform.position), spider.getGroundNormal());
        if(Math.Abs(Vector3.Angle(spider.transform.forward, targetFacing)) > error) {
            spider.turn(targetFacing);
        }
        
        //the spider must constantly walk, not in an else
        spider.walk(spider.transform.forward);
    }
}
