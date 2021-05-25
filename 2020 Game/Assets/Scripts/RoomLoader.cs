using UnityEngine;

//Script should be added onto player object
public class RoomLoader : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject hallway;
    public int depth;   // How many rooms the player sees *Must to be odd*
    private Room root;
 
    void Awake() 
    {
        Random.InitState(39);
        GameObject firstRoom = Instantiate(GetNextRoom(), new Vector3(0, 0, 0), Quaternion.identity);
        root = new Room(firstRoom, firstRoom.transform.childCount-1, depth);
        CreateInitialRooms(root, 0);
    }

    void OnCollisionEnter(Collision col) 
    {
        Room middleRoom = FindCollidedRoom(col, root, 0);
        if (middleRoom != null)
        {
            int[] branchNums = middleRoom.GetBranch();

            //Debug.Log("Received Object");   // Tested this works
            //foreach (int b in branchNums)
            //    Debug.Log(b);

            DeleteBranches(root, branchNums, 0);
            Room temp = root.GetChildRooms()[branchNums[0]];
            Destroy(root.room);
            root.room = null;
            root = temp;        // New Root

            CreateNewRoomLayer(root);
        }
    }

    //========================================================================================== Room Creation
    private void CreateInitialRooms(Room room, int layers)
    {
        layers++;
        if(layers == depth)
        {
            return;
        }
        Debug.Log(room.GetRoomInfo());
        foreach(Room r in AddNewRoom(room))
        {
            CreateInitialRooms(r, layers);
        }
    }

    /* 
     * Add new rooms to each exit on last layer. 
     * Used during gameplay
     */
    private void CreateNewRoomLayer(Room room)
    {
        if(room.GetChildRooms()[0] == null)
        {
            AddNewRoom(room);
            return;
        } 

        foreach(Room r in room.GetChildRooms())
        {
            CreateNewRoomLayer(r);
        }
    }

    /*
     * Takes previous room node and creates next rooms
     */
    private Room[] AddNewRoom(Room previousRoom)
    {
        Room[] exitRooms = new Room[previousRoom.room.transform.childCount - 1];

        Transform previousTransform = previousRoom.room.transform;
        for (int i = 1; i < previousTransform.childCount; i++)
        {
            GameObject nextRoom = GetNextRoom();
            Transform nextStart = nextRoom.transform.GetChild(0).gameObject.transform;
            Transform previousEnd = previousTransform.GetChild(i).transform;
            float rotation = previousEnd.rotation.eulerAngles.y - nextStart.rotation.eulerAngles.y;

            Vector3 nextRoomLocation = previousEnd.position;
            if (rotation != nextRoom.GetComponent<Transform>().rotation.eulerAngles.y)
            {
                Vector3 rotatedCoords = GetRotatedCoordinates(rotation, nextStart.position);
                nextRoomLocation -= rotatedCoords;

                //Debug.Log(previousEnd.position + " " + nextStart.position);
                //Debug.Log(rotatedCoords);
                //Debug.Log(previousEnd.rotation.eulerAngles.y + " " + nextStart.rotation.eulerAngles.y);
            }
            else
            {
                nextRoomLocation -= nextStart.position;
            }
            previousRoom.AddChild(Instantiate(nextRoom, nextRoomLocation, Quaternion.Euler(0, rotation, 0)));
            exitRooms[i - 1] = previousRoom.GetChild(i - 1);
        }

        return exitRooms;
    }

    //========================================================================================== Room generation and deletion

    private Room FindCollidedRoom(Collision col, Room room, int layer)
    {
        if (layer > depth / 2 - 1)
        {
            Debug.Log("Passed Midpoint ending loop");
            return null;
        }

        Room[] temp = room.GetChildRooms();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].SetBranch(layer, i);

            if (col.gameObject.Equals(temp[i].room))
            {
                Debug.Log("Found collided object");
                if (layer == depth / 2 - 1)
                {
                    // Middle collided object found do something !
                    Debug.Log("You hit middle room " + temp[i].GetRoomInfo());
                    return temp[i];
                }
            }

            return FindCollidedRoom(col, temp[i], layer + 1);
        }

        return null;
    }

    private void DeleteBranches(Room room, int[] branch, int index)
    {
        if (index == branch.Length)
        {
            return;
        }

        Room[] tempRooms = room.GetChildRooms();
        for(int i = 0; i < tempRooms.Length; i++)
        {
            if (i == branch[index])
            {
                continue;
            }
            else
            {
                DestroyRooms(tempRooms[i]);
                //Destroy(tempRooms[i].room);
                //room.DeleteChildRoom(i, true);
            }
        }

        DeleteBranches(tempRooms[branch[index]], branch, index++);
    }

    /*
     * Destroys all rooms connected to the given room
     */
    private void DestroyRooms(Room room)  
    {
        if(room == null)
        {
            return;
        }

        Room[] childRooms = room.GetChildRooms();
        for(int i = 0; i < childRooms.Length; i++)
        {
            DestroyRooms(childRooms[i]);
            if(childRooms[i] != null)
            {
                Destroy(childRooms[i].room);
                room.DeleteChildRoom(i, true);  //Always 
            }

        }
    }

    //========================================================================================== Helper

    private Vector3 GetRotatedCoordinates(float deg, Vector3 position)
    {
        float rad = -deg * Mathf.Deg2Rad;
        float new_z = -1 * ((-position.z) * Mathf.Cos(rad) - position.x * Mathf.Sin(rad));
        float new_x = (-position.z) * Mathf.Sin(rad) + position.x * Mathf.Cos(rad);
        return new Vector3(new_x, position.y, new_z);
    }

    private GameObject GetNextRoom() 
    {
        int randomRoom = Random.Range(0, rooms.Length);
        return rooms[randomRoom];
    }

}
