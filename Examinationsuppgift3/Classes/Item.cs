using Examinationsuppgift3.Helper_Classes;


namespace Examinationsuppgift3.Classes;

public class Item : Entity
{
    public bool IsMovable { get; private set; }
    public Room Room { get; private set; }
    
    public Item(string name, string description, bool isMovable, Room room) : base(name, description)
    {
        Name = name;
        Description = description;
        IsMovable = isMovable;
        Room = room;
    }
}