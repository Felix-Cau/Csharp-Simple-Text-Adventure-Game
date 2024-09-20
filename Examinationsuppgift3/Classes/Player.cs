using Examinationsuppgift3.Helper_Classes;

namespace Examinationsuppgift3.Classes;

public class Player : Entity
{
    List<Item> ItemsOnThePlayer { get; set; }
    string[] ActionStatus { get; set; }

    public void SetActionStatus(string[] userInput)
    {
        ActionStatus = userInput;
    }
}