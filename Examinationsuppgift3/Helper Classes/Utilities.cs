using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class Utilities
{
    public static (bool, string doorName) CheckForDoorConnectedToAction(string[] userInputAsArray)
    {
        var doorConnectedToAction = FileHandler.ReadObjectsInFile<Door>().OfType<Door>().FirstOrDefault(door => door.Name.ToLower() == userInputAsArray[1] || 
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
        var itemConnectedToAction = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().FirstOrDefault(item => item.Name.ToLower() == userInputAsArray[1] || 
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
        var targetItem = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().FirstOrDefault(item => item.Name.ToLower() == userInputAsArray[3]);
        
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
        var targetRoom = FileHandler.ReadObjectsInFile<Room>().OfType<Room>().FirstOrDefault(room => room.Name.ToLower() == userInputAsArray[1] || 
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
}