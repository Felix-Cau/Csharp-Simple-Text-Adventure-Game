using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Item : Entity
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
}