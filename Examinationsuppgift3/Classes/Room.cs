using System.Reflection.Emit;

namespace Examinationsuppgift3.Classes;

public class Room : Entity
{
    public Room(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }
}