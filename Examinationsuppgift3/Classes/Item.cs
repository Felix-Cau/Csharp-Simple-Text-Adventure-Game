using Examinationsuppgift3.Helper_Classes;


namespace Examinationsuppgift3.Classes;

public class Item : Entity
{
    public string ObjectType => this.GetType().Name;
    public bool IsMovable { get; private set; }
    public Room Room { get; private set; }
    
    public Item(string name, string description, bool isMovable, Room room) : base(name, description)
    {
        IsMovable = isMovable;
        Room = room;
    }

    public void ChangeRoom(string newRoom)
    {
        var localNewRoom = Repository.AllObjectsInGame.OfType<Room>().FirstOrDefault(x => x.Name == newRoom);
        Room = localNewRoom;
    }
}