using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Door : Item
{
    public bool IsLocked { get; set; }
    
    public Door(string name, string description, bool isMovable, Room room, bool isLocked) : base(name, description, isMovable, room)
    {
        IsLocked = isLocked;
    }

    public void UnlockDoor()
    {
        IsLocked = false;
    }

    public void LockDoor()
    {
        IsLocked = true;
    }
}