using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Player : Entity
{
    public string ObjectType => this.GetType().Name;
    public string ActionStatus { get; private set; }
    public List<Item> ItemsOnThePlayer { get; private set; }
        // Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == "On Person").ToList();
        // = FileHandler.ReadObjectsInFile<Item>().OfType<Item>()
        // .Where(x => x.Room.Name == "On Person").ToList();

    public Room CurrentRoom { get; private set; } 
            // Repository.AllObjectsInGame.OfType<Room>().Where(room => room.Name == "Bar").SingleOrDefault();
        // = FileHandler.ReadObjectsInFile<Room>().OfType<Room>().FirstOrDefault(x => x.Name == "Bar");

    public Player(string name, string description) : base(name, description)
    {
    }

    public void SetActionStatus(string userInput)
    {
        ActionStatus = userInput;
    }

    public void SetItemsOnThePlayerAtStartUp()
    {
        ItemsOnThePlayer = Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == "On Person").ToList();
    }

    public void SetCurrentRoomAtStartUp()
    {
        CurrentRoom = Repository.AllObjectsInGame.OfType<Room>().FirstOrDefault(x => x.Name == "Bar");
    }

    public void ChangeCurrentRoom(Room newRoom)
    {
        CurrentRoom = newRoom;
    }
}