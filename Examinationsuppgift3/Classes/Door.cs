using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Door : Item
{
    public Door(string name, string description, Room room, bool isLocked, List<Room> connectedRoom) : base(name, description, room)
    {
        IsLocked = isLocked;
        ConnectedRoom = connectedRoom;
    }

    bool IsLocked { get; set; }
    public List<Room> ConnectedRoom { get; private set; }
    // public static List<Door> AllDoors { get; private set; } = FileHandler.UnfilteredEntities.OfType<Door>().ToList();

    public void UnlockDoor()
    {
        IsLocked = false;
    }


}