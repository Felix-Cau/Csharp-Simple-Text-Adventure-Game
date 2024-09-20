namespace Examinationsuppgift3.Classes;

public class Item : Entity
{
    public bool IsMovable { get; set; }
    public Room Room { get; set; }

    public Item(string name, string description, bool isMovable, Room room) : base(name, description)
    {
        Name = name;
        Description = description;
        IsMovable = isMovable;
        Room = room;
    }
}