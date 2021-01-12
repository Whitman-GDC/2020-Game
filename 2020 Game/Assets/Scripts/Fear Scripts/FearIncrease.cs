using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearIncrease : MonoBehaviour
{
    public float fearup;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void IncreaseFear()
    {

        FearLevel.currentfear += fearup;

    }


}
