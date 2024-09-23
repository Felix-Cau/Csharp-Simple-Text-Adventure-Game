using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public class EventResolver
{
    public Player ResolveEvents(Player player, Room room, string[] userInputAsArray)
    {
        player = CheckForActionKeywords(player, userInputAsArray);

        if (player.ActionStatus == "use")
        {
            (bool doesItemExist, string itemName) = CheckForItemConnectedToAction(userInputAsArray);
            if (!doesItemExist)
            {
                Console.WriteLine("Error, the item you tried to use doesn't exist.");
            }
            else
            {
                (bool doesTargetItemExist, string targetItemName) = CheckForTargetItem<Item>(userInputAsArray);
                if (!doesTargetItemExist)
                {
                    Console.WriteLine("Error, the target item you tried to use doesn't exist.");
                }
                else
                {
                    var targetItemObject = FileHandler.UnfilteredEntities.FirstOrDefault(x => x.Name == targetItemName);
                    if (itemName.ToLower() == "key" && targetItemObject is Door door)
                    {
                        door.UnlockDoor();
                        player.ChangeCurrentRoom(door.Name);
                        return player;
                    }
                    else if (itemName.ToLower() == "key" && targetItemObject is not Door doorItem)
                    {
                        Console.WriteLine("The target item you tried to use a key on is not a door.");
                    }
                    else
                    {
                        Console.WriteLine($"There is no way to interact with {itemName} on {targetItemName}");
                    }
                }
            }
        }
        else if (player.ActionStatus == "get")
        {
            (bool doesItemExist, string itemName) = CheckForItemConnectedToAction(userInputAsArray);
            if (!doesItemExist)
            {
                Console.WriteLine("Error, the item you tried to get doesn't exist.");
            }
            else
            {
                var item = FileHandler.UnfilteredEntities.FirstOrDefault(x => x.Name == itemName);
                FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(item, item.Name);
                    //måste fixa redigering av dbFilen
            }
            
        }
        else if (player.ActionStatus == "drop")
        {
            
        }
        else if (player.ActionStatus == "inspect")
        {
            
        }
        else if (player.ActionStatus == "move")
        {
            
        }
        else
        {
            Console.WriteLine($"{player.ActionStatus}");
        }
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
    
    private (bool, string itemName) CheckForTargetItem<T>(string[] userInputAsArray) where T : class
    {
        T targetItem = FileHandler.UnfilteredEntities.OfType<T>()
            .FirstOrDefault(x => (x as dynamic).Name == userInputAsArray[3]);
        
        if (targetItem == null)
        {
            return (false, "Item does not exist.");
        }
        else
        {
            return (true, (targetItem as dynamic).Name);
        }
    }
}