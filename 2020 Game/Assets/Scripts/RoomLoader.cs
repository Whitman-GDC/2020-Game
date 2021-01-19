using UnityEngine;
using System.Collections.Generic;

//Script should be added onto player object
public class RoomLoader : MonoBehaviour
{
    public readonly GameObject[] rooms; 
    public int depth;   // How many rooms the player sees *Must to be odd*
    private List<GameObject> activeRooms;
    private Vector3 anchorPoint;
 
    void Awake() 
    {   
        /*  
            Uses empty child objects as anchor points.
        */
        anchorPoint = new Vector3(0, 0, 0);     // The coordinate where the next room should go
        activeRooms = new List<GameObject>();               
        activeRooms.Add(Instantiate(getNextRoom(), anchorPoint, Quaternion.identity));
        for(int i = 1; i < depth; i++) 
        {
            addNewRoom();
        }
    }

    void OnTriggerEnter(Collider col) 
    {   
        // The room that collides with player is the next room (middle)
        if(activeRooms.IndexOf(col.gameObject) == ((depth-1)/2+1)) 
        {
            addNewRoom();
            activeRooms.RemoveAt(0);
        }
    }

    private void addNewRoom()
    {
        GameObject nextRoom = getNextRoom();        // Two empty game objects 0 is entrance 1 is exit
        Transform startPoint = nextRoom.transform.GetChild(0).gameObject.GetComponent<Transform>();
        Transform endPoint = activeRooms[activeRooms.Count-1].transform.GetChild(1).gameObject.GetComponent<Transform>();
        anchorPoint = anchorPoint + endPoint.position + (-startPoint.position);
        activeRooms.Add(Instantiate(nextRoom, anchorPoint, Quaternion.identity));   // Currently assuming all rooms are lined up
    }

    private GameObject getNextRoom() 
    {
        int randomRoom = Random.Range(0, rooms.Length-1);
        return rooms[randomRoom];
    }

}
