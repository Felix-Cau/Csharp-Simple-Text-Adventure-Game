using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public class EventResolver
{
    public void ResolveEvents(Player player, Room room, string[] userInputAsArray)
    {
        player = CheckForActionKeywords(player, userInputAsArray);
    }

    private Player CheckForActionKeywords(Player player, string[] userInputAsArray)
    {
        switch (userInputAsArray[0])
        {
            case "use":
                player.SetActionStatus("use");
                break;
            case "get":
                player.SetActionStatus("get");
                break;
            case "drop":
                player.SetActionStatus("drop");
                break;
            case "inspect":
                player.SetActionStatus("inspect");
                break;
            case "move":
                player.SetActionStatus("move");
                break;
            default:
                player.SetActionStatus("Invalid command. Please try again.");
                break;
        }
        return player;
    }

    private (bool, string itemName) CheckForItemConnectedToAction(string[] userInputAsArray)
    {
        var itemConnectedToAction = FileHandler.UnfilteredEntities.OfType<Item>().FirstOrDefault(item => item.Name == userInputAsArray[1]);

        if (itemConnectedToAction == null)
        {
            return (false, "Item does not exist.");
        }
        else
        {
            return (true, itemConnectedToAction.Name);
        }
    }
    
    private (bool, string itemName) CheckForTargetItem(string[] userInputAsArray)
    {
        // var itemConnectedToAction = 
        List<Item> targetItem = new();

        for (int i = 2; i < userInputAsArray.Length; i++)
        {
            targetItem.Add(FileHandler.UnfilteredEntities.OfType<Item>().FirstOrDefault(item => item.Name == userInputAsArray[i]));
        }
            

        if (targetItem == null)
        {
            return (false, "Item does not exist.");
        }
        else
        {
            return (true, itemConnectedToAction.Name);
        }
    }
}