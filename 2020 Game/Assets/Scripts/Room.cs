using UnityEngine;

public class Room
{

    public GameObject room;
    private Room[] childRoomNodes;
    private int arrIndex;
    private int[] branchNums;
    private int depth;

    public Room(GameObject room, int exitCount, int depth)
    {
        childRoomNodes = new Room[exitCount];
        this.room = room;
        this.depth = depth;
        arrIndex = 0;
        branchNums = new int[depth];
    }

    public void AddChild(GameObject room)
    {
        //childRoomNodes.Add(new Room(room));
        childRoomNodes[arrIndex] = new Room(room, room.transform.childCount-1, depth);
        arrIndex++;
    }

    public Room GetChild(int index) 
    {
        return childRoomNodes[index];
    }
    
    public Room[] GetChildRooms()
    {
        return childRoomNodes;
    }

    public void DeleteChildRoom(int index, bool shift)
    {
        childRoomNodes[index] = null;
        if(shift)
        {
            Room[] copy = (Room[]) childRoomNodes.Clone();
            childRoomNodes = new Room[childRoomNodes.Length - 1];
            for(int i = 0; i < childRoomNodes.Length; i++)
            {
                if(i < index)
                {
                    childRoomNodes[i] = copy[i];
                } 
                else
                {
                    childRoomNodes[i] = copy[i + 1];
                }
            }
        }
    }

    public void SetBranch(int layer, int branch)
    {
        branchNums[layer] = branch;
    }

    public int[] GetBranch()
    {
        return branchNums;
    }

    public string GetRoomInfo()
    {
        return room.name + "\nExits : " + childRoomNodes.Length.ToString() + "\nPosition : " + room.transform.position.ToString() + "\nRotation : " + room.transform.rotation.eulerAngles.ToString();
    }

}