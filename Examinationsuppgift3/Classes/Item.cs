using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Item : Entity, ISavable
{
    public bool IsMovable { get; private set; }
    public Room Room { get; private set; }
    
    public static List<Item> Items { get; set; } = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().ToList();

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

    public void SaveObjectToFile<T>(T item)
    {
        FileHandler.SaveObjectToFile(item);
    }
}