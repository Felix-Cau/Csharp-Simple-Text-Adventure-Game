using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Item : Entity, ISavable, ILoadable
{
    private bool IsMovable { get; set; }
    public Room Room { get; private set; }
    
    public static List<Item> Items { get; set; } = FileHandler.UnfilteredEntities.OfType<Item>().ToList();

    public Item(string name, string description, bool isMovable, Room room) : base(name, description)
    {
        Name = name;
        Description = description;
        IsMovable = isMovable;
        Room = room;
    }

    public Item(string name, string description, Room room) : base(name, description)
    {
        Name = name;
        Description = description;
        Room = room;
    }

    public void SaveObjectToFile<Item>(Item item)
    {
        FileHandler.SaveObjectToFile(item);
    }

    public List<Item> LoadObject<Item>() where Item : class
    {
        var localFilteredListToItems = FileHandler.ReadObjectsInFile<Entity>().OfType<Item>().ToList();
        
        return localFilteredListToItems;
    }
}