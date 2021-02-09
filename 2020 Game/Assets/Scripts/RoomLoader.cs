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
        activeRooms = new List<GameObject>
        {
            Instantiate(GetNextRoom(), new Vector3(0, 0, 0), Quaternion.identity)
        };
        for (int i = 1; i < depth; i++) 
        {
            AddNewRoom();
        }
    }

    void OnCollisionEnter(Collision col) 
    {   
        // The room that collides with player is the next room (middle)
        if(activeRooms.IndexOf(col.gameObject) == (depth-1)/2)
        {
            AddNewRoom();
            Destroy(activeRooms[0]);
            activeRooms.RemoveAt(0);
        }
    }

    private void AddNewRoom()
    {
        GameObject nextRoom = GetNextRoom();        // Two empty game objects 0 is entrance, 1 is exit
        Transform nextStart = nextRoom.transform.GetChild(0).gameObject.GetComponent<Transform>();
        Transform previousEnd = activeRooms[activeRooms.Count-1].transform.GetChild(1).gameObject.GetComponent<Transform>();
                        //Global
        float rotation = previousEnd.rotation.eulerAngles.y - nextStart.rotation.eulerAngles.y;     // Something wrong here

        // Position of the next room have to take into account of its rotation
        Vector3 nextRoomLocation = previousEnd.position;
        if(rotation != nextRoom.GetComponent<Transform>().rotation.eulerAngles.y) 
        {
            Debug.Log(previousEnd.position + " " + nextStart.position);
            nextRoomLocation += GetRotatedCoordinates(rotation, nextStart.position);
            Debug.Log(GetRotatedCoordinates(rotation, nextStart.position));
        } 
        else 
        {
            nextRoomLocation -= nextStart.position;
        }

        Quaternion nextRoomRotation = Quaternion.Euler(0, rotation, 0);

        activeRooms.Add(Instantiate(nextRoom, nextRoomLocation, nextRoomRotation));   // Can take one rotation, but not the second.
    }
    
    private Vector3 GetRotatedCoordinates(float deg, Vector3 position)
    {
        float rad = deg * Mathf.Deg2Rad;
        // return new Vector3(position.x * Mathf.Sin(rad) + (-position.z) * Mathf.Cos(rad), 0, 
        // (position.x * Mathf.Cos(rad) - (-position.z) * Mathf.Sin(rad)) * -1);
        return new Vector3((-position.z) * Mathf.Sin(rad) + position.x * Mathf.Cos(rad), 0, 
        -1 * ((-position.z) * Mathf.Cos(rad) - position.x * Mathf.Sin(rad)));
        // x' = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
        // y' = x * Mathf.Sin(rad) + y * Mathf.Cos(rad);
    }

    private GameObject GetNextRoom() 
    {
        int randomRoom = Random.Range(0, rooms.Length);
        return rooms[randomRoom];
    }

}
