using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public float fearup;
    public float range;
    public Transform player;
    bool IsLight;
    void Start()
    {
        InvokeRepeating("CheckForLight", 1.0f, 1.0f);
        //repeats the checkforlight loop
        IsLight = false;
        
    }
    void CheckForLight()
    {
        Vector3 raycastdirection = player.transform.position - transform.position;
        // gets the direction of the raycast 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastdirection, out hit, range))
            if(hit.transform.gameObject.layer == 8)
            // if the raycast hits an object on layer 8 return true
            {IsLight = true;
            Debug.Log(hit.transform.name);
            }
            else { IsLight = false; }


        if (IsLight == false) {FearLevel.currentfear += fearup;}
      else {FearLevel.currentfear -= fearup;}
        //adds/subtracts the fear level based on the bool IsLight
    }
}
