using UnityEngine;
using System.Collections.Generic;

//Script should be added onto player object
public class RoomLoader : MonoBehaviour
{
    public GameObject[] rooms; 
    public int depth;   // How many rooms the player sees *Must to be odd*
    private List<GameObject> activeRooms;
 
    void Awake() 
    {   
        /*  
            Uses empty child objects as anchor points.
        */
        activeRooms = new List<GameObject>();               
        activeRooms.Add(Instantiate(getNextRoom(), new Vector3(0, 0, 0), Quaternion.identity));
        for(int i = 1; i < depth; i++) 
        {
            addNewRoom();
        }
    }

    void OnCollisionEnter(Collision col) 
    {   
        Debug.Log(col.gameObject.name);
        // The room that collides with player is the next room (middle)
        if(activeRooms.IndexOf(col.gameObject) == (depth-1)/2)
        {
            addNewRoom();
            Destroy(activeRooms[0]);
            activeRooms.RemoveAt(0);
        }
    }

    private void addNewRoom()
    {
        GameObject nextRoom = getNextRoom();        // Two empty game objects 0 is entrance, 1 is exit
        Vector3 startPoint = nextRoom.transform.GetChild(0).gameObject.GetComponent<Transform>().position;
        Vector3 endPoint = activeRooms[activeRooms.Count-1].transform.GetChild(1).gameObject.GetComponent<Transform>().position;
        activeRooms.Add(Instantiate(nextRoom, (endPoint + (-startPoint)), Quaternion.identity));   // Currently assuming all rooms are lined up
    }

    private GameObject getNextRoom() 
    {
        int randomRoom = Random.Range(0, rooms.Length);
        return rooms[randomRoom];
    }

}
