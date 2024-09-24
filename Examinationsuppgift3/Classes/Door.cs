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

    public void UnlockDoor()
    {
        IsLocked = false;
    }

    public void SaveObjectToFile<Door>(Door obj)
    {
        FileHandler.SaveObjectToFile(obj);
    }

    public List<Door> LoadObject<Door>() where Door : class
    {
        var localFilteredListToDoors = FileHandler.ReadObjectsInFile<Entity>().OfType<Door>().ToList();
        
        return localFilteredListToDoors;
    }
}