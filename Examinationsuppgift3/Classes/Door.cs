using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Door : Item, ISavable, ILoadable
{
    public Door(string name, string description, Room room, bool isLocked) : base(name, description, room)
    {
        IsLocked = isLocked;
    }

    bool IsLocked { get; set; }
    public Room ConnectedRoom { get; private set; }
    public Item KeyItem { get; private set; }
    public List<Door> AllDoors { get; private set; } = FileHandler.UnfilteredEntities.OfType<Door>().ToList();

    public void UnlockDoor()
    {
        IsLocked = false;
    }

    public void SaveObjectToFile(Entity door)
    {
        FileHandler.SaveObjectToFile(door);
    }

    public void LoadObject()
    {
        var localFilteredListToDoors = FileHandler.ReadObjectsInFile().OfType<Door>().ToList();
        
        AllDoors = localFilteredListToDoors;
    }
}