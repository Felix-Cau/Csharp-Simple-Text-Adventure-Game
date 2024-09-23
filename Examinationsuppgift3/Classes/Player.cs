using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Player : Entity
{
    public List<Item> ItemsOnThePlayer { get; private set; } = FileHandler.UnfilteredEntities.OfType<Item>()
                                                                .Where(x => x.Room.Name == "onPlayer").ToList();
    public string ActionStatus { get; private set; }
    
    public Room CurrentRoom { get; private set; } = FileHandler.UnfilteredEntities.OfType<Room>().FirstOrDefault(x => x.Name == "Bar");

    public Player(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public void SetActionStatus(string userInput)
    {
        ActionStatus = userInput;
    }

    public void LoadItemsOnThePlayer()
    {
        
    }
    public List<Item> GetItemsOnThePlayer()
    {
        return ItemsOnThePlayer;
    }

    public void ChangeCurrentRoom(string newRoomName)
    {
        CurrentRoom = FileHandler.UnfilteredEntities.OfType<Room>().FirstOrDefault(x => x.Name == newRoomName);
    }
}