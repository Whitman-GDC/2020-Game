using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FearLevel : MonoBehaviour
{
    public Slider fearslider;
    public float startfear;
    public static float currentfear;


    void Start()
    {
        currentfear = startfear;
    }

    void Update()
    {
        fearslider.value = currentfear;
    }
}
