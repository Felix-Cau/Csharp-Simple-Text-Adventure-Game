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
                (bool doesTargetItemExist, string targetItemName) = CheckForTargetItem(userInputAsArray);
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
                    else if (itemName.ToLower() == "key" && targetItemObject is not Door)
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
            else if (player.CurrentRoom == room)
            {
                var itemToUpdate = FileHandler.UnfilteredEntities.OfType<Item>().FirstOrDefault(x => x.Name == itemName);
                itemToUpdate.Room.Name = "OnPerson";
                FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(itemToUpdate, itemToUpdate.Name);
                Console.WriteLine("You got it now.");
            }
            else
            {
                Console.WriteLine("There is no way to interact with that object as it isn" +
                                  "t in your room.");
            }
        }
        else if (player.ActionStatus == "drop")
        {
            (bool doesItemExist, string itemName) = CheckForItemConnectedToAction(userInputAsArray);
            if (!doesItemExist)
            {
                Console.WriteLine("Error, the item you tried to drop doesn't exist.");
            }
            else
            {
                var itemToUpdate = player.ItemsOnThePlayer.FirstOrDefault(x => x.Name == itemName);
                if (itemToUpdate is null)
                {
                    itemToUpdate.Room.Name = room.Name;
                    FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(itemToUpdate, itemToUpdate.Name);
                    Console.WriteLine("You dropped the item.");
                }
                else
                {
                    Console.WriteLine("Something went wrong, you can't drop the item.");
                }
            }
        }
        else if (player.ActionStatus == "inspect")
        {
            (bool doesItemExist, string itemName) = CheckForItemConnectedToAction(userInputAsArray);
            var itemToInspect = room.ItemsInRoom.FirstOrDefault(x => x.Name == itemName);
            
            if (!doesItemExist)
            {
                Console.WriteLine("Error, the item you tried to inspect doesn't exist.");
            }
            else if (itemToInspect is not null)
            {
                Console.WriteLine("You inspect the item.\n");
                Console.WriteLine(itemToInspect.Description);
            }
            else
            {
                Console.WriteLine("The item you try to inspect doesn't exist.");
            }
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
    
    private (bool, string itemName) CheckForTargetItem(string[] userInputAsArray)
    {
        var targetItem = FileHandler.UnfilteredEntities.OfType<Item>().FirstOrDefault(item => item.Name == userInputAsArray[3]);
        
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