using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Item : Entity, ISavable, ILoadable
{
    public bool IsMovable { get; private set; }
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

    public void SaveObjectToFile(Entity item)
    {
        FileHandler.SaveObjectToFile(item);
    }

    public void LoadObject()
    {
        var localFilteredListToItems = FileHandler.ReadObjectsInFile().OfType<Item>().ToList();
        
        Items = localFilteredListToItems;
    }
}