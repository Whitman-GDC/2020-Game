using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Add this script to the field of view objects of enemies. If the player enters the attached object's collider,
//the script sees if the enemy has a clear line of sight to the player and ends the game if it does.
public class SeePlayer : MonoBehaviour
{
    public Transform player;
    public GameEnder gameEnder;

    bool IsPlayerInRange;

    //These two methods update the IsPlayerInRange boolean
    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        //If the player is in range, determine if there is a clear line of sight to the player. If there is, end the game+ Vector3.up
        if (IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnder.Caught();                    
                }
            }
        }
    }
}
