using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class Utilities
{
    public static (bool, string doorName) CheckForDoorConnectedToAction(string[] userInputAsArray)
    {
        var doorConnectedToAction = Repository.AllObjectsInGame.OfType<Door>().FirstOrDefault(door => door.Name.ToLower() == userInputAsArray[1] || 
                                                                                              door.Name.ToLower() == userInputAsArray[2]);

        if (doorConnectedToAction == null)
        {
            return (false, "Door not found");
        }
        else
        {
            return (true, doorConnectedToAction.Name.ToLower());
        }
    }
    public static (bool, string itemName) CheckForItemConnectedToAction(string[] userInputAsArray)
    {
        var itemConnectedToAction = Repository.AllObjectsInGame.OfType<Item>().FirstOrDefault(item => item.Name.ToLower() == userInputAsArray[1] || 
            item.Name.ToLower() == userInputAsArray[2]);

        if (itemConnectedToAction == null)
        {
            return (false, "Item does not exist.");
        }
        else
        {
            return (true, itemConnectedToAction.Name);
        }
    }
    
    public static (bool, string itemName) CheckForTargetItem(string[] userInputAsArray)
    {
        var targetItem = Repository.AllObjectsInGame.OfType<Item>().FirstOrDefault(item => item.Name.ToLower() == userInputAsArray[3]);
        
        if (targetItem == null)
        {
            return (false, "Item does not exist.");
        }
        else
        {
            return (true, targetItem.Name);
        }
    }

    public static (bool, string roomName) CheckForTargetRoom(string[] userInputAsArray)
    {
        var targetRoom = Repository.AllObjectsInGame.OfType<Room>().FirstOrDefault(room => room.Name.ToLower() == userInputAsArray[1] || 
            room.Name.ToLower() == userInputAsArray[2] ||
            room.Name.ToLower() == userInputAsArray[3]);
        
        if (targetRoom == null)
        {
            return (false, "Room does not exist.");
        }
        else
        {
            return (true, targetRoom.Name);
        }
    }

    public static (bool, string roomName) CheckIfTargetIsPlayer(string[] userInputAsArray)
    {
        if (userInputAsArray[1] == "player" || userInputAsArray[2] == "player" || userInputAsArray[1] == "person" || userInputAsArray[2] == "person")
        {
            var targetPlayer = Repository.AllObjectsInGame.OfType<Room>()
                .FirstOrDefault(room => room.Name.Contains("Person"));
            return (true, targetPlayer.Name);
        }
        else
        {
            return (false, "Didn't work, try again.");
        }
    }
}
