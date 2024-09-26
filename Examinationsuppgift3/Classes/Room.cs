using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;


namespace Examinationsuppgift3.Classes;

public class Room : Entity
{
    public string ObjectType => this.GetType().Name;
    public Room(string name, string description) : base(name, description)
    {
    }
}