using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Room : Entity
{
    public static List<Room> Rooms { get; private set; } = FileHandler.UnfilteredEntities.OfType<Room>().ToList();
    
    public Room(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }
}