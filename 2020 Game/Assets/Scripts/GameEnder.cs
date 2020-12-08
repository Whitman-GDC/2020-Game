using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Call the correct game end method if the game should end
public class GameEnder : MonoBehaviour
{
    public void Caught()
    {
        //Do something here to show that the game ended (reset level etc)
        //For now just quit
        Debug.Log("You were caught; quiting game");
        Application.Quit ();
    }
}
