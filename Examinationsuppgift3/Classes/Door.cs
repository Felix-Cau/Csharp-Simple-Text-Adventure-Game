using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Door : Item
{
    public bool IsLocked { get; set; }
    public List<Room> ConnectedRoom { get; private set; }
    
    public Door(string name, string description, Room room, bool isLocked, List<Room> connectedRoom) : base(name, description, room)
    {
        IsLocked = isLocked;
        ConnectedRoom = connectedRoom;
    }

    public void UnlockDoor()
    {
        IsLocked = false;
    }
}