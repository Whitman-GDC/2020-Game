using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLegPosition : MonoBehaviour
{
    public Transform joint1;
    public Transform joint2;

    private Transform transform;

    void Start() 
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (joint1.position + joint2.position)/2;
        transform.LookAt(joint2);
    }
}
