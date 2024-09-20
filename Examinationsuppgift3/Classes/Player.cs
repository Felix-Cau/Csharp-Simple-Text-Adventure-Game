using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Player : Entity
{
    private List<Item> ItemsOnThePlayer { get; set; }
    private string[] ActionStatus { get; set; }

    public Player(string name, string description,List<Item> itemsOnThePlayer, string[] actionStatus) : base(name, description)
    {
        Name = name;
        Description = description;
        ItemsOnThePlayer = itemsOnThePlayer;
        ActionStatus = actionStatus;
    }

    public void SetActionStatus(string[] userInput)
    {
        ActionStatus = userInput;
    }

    public List<Item> GetItemsOnThePlayer()
    {
        return ItemsOnThePlayer;
    }
}