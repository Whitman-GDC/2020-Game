using UnityEngine;

public class SunMotion : MonoBehaviour
{
    public float radius;
    public float rateOfTime;
    private float time;     
    private float currentTime;
    // private float duskDawnTime;

    private void Start()
    {
        radius = 800f;
        rateOfTime = 1f;
        time = 1000f;            // Day cycles every 1000 update when rate = 1
        // duskDawnTime = 50f;
        currentTime = time;
        transform.position = new Vector3(0, radius, 0);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(radius * Mathf.Cos((currentTime/time) * 2 * Mathf.PI), 
                                            radius * Mathf.Sin((currentTime/time) * 2 * Mathf.PI), 0f);
        transform.LookAt(new Vector3(0,0,0));
        
        /*
        if(time == 0)  
        {
            // Start dusk
        } 
        else if(time >= duskDawnTime) 
        {
            // End dusk
        }
        else if(time >= time/2-duskDawnTime)
        {
            // Start dawn
        }
        */

        if(currentTime > time)
        {
            currentTime = 0f;
        } else {
            currentTime += rateOfTime;
        }
    }

}
