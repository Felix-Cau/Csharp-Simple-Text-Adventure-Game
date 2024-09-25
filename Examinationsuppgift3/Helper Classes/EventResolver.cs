using System.ComponentModel.Design;
using System.Diagnostics;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class EventResolver
{
    public static (bool, Player) ResolveEvents(Player player, string[] userInputAsArray)
    {
        bool localKeepGameLoopGoing = true;
        player = UserInputHandler.CheckForActionKeywords(player, userInputAsArray);
        var room = Room.Rooms.Where(room => room.Name == player.CurrentRoom.Name).SingleOrDefault();

        if (player.ActionStatus == "use")
        {
            (bool doesDoorExist, string doorName) = Utilities.CheckForDoorConnectedToAction(userInputAsArray);
            
            if (doesDoorExist)
            {
                switch (doorName)
                {
                    case "door from the bar":
                        var localUpdatePlayerCurrentRoomWithHallway = FileHandler.ReadObjectsInFile<Room>().OfType<Room>()
                                                                      .Where(room => room.Name == "Hallway").SingleOrDefault();
                        player.ChangeCurrentRoom(localUpdatePlayerCurrentRoomWithHallway);
                        Console.WriteLine("You have entered the hallway.");
                        break;
                    case "door back to the bar":
                        var localUpdatePlayerCurrentRoomWithBar = FileHandler.ReadObjectsInFile<Room>().OfType<Room>()
                                                                  .Where(room => room.Name == "Bar").SingleOrDefault();
                        player.ChangeCurrentRoom(localUpdatePlayerCurrentRoomWithBar);
                        Console.WriteLine("You have gone back to the bar.");
                        break;
                    case "mysterious door":
                        var localUpdatePlayerCurrentRoomWithEndRoom = FileHandler.ReadObjectsInFile<Room>().OfType<Room>()
                                                                      .Where(room => room.Name == "EndRoom").SingleOrDefault();
                        var localDarkEndRoomObject = FileHandler.ReadObjectsInFile<Door>().OfType<Door>()
                                                     .Where(door => door.Name == "Mysterious door").SingleOrDefault();

                        if (localDarkEndRoomObject.IsLocked)
                        {
                            Console.WriteLine("You are trying to open a locked door. Find a key and use it on the door.");
                        }
                        else if (!localDarkEndRoomObject.IsLocked)
                        {
                            player.ChangeCurrentRoom(localUpdatePlayerCurrentRoomWithEndRoom);
                            Console.WriteLine("You have entered the last room in this game.\n");
                            Console.WriteLine(localUpdatePlayerCurrentRoomWithEndRoom.Description);
                            localKeepGameLoopGoing = false;
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.WriteLine("Something went wrong. Try to use the door again.");
                        break;
                }
            }
            else
            {
                (bool doesItemExist, string itemName) = Utilities.CheckForItemConnectedToAction(userInputAsArray);
            
                if (!doesItemExist)
                {
                    Console.WriteLine("Error, the item you tried to use doesn't exist.");
                }
                else
                {
                    (bool doesTargetItemExist, string targetItemName) = Utilities.CheckForTargetItem(userInputAsArray);
                    if (!doesTargetItemExist)
                    {
                        Console.WriteLine("Error, the target item you tried to use doesn't exist.");
                    }
                    else
                    {
                        var targetItemObject = FileHandler.ReadObjectsInFile<Door>().OfType<Door>().Where(door => door.Name.ToLower() == targetItemName).SingleOrDefault();
                        if (itemName.ToLower() == "key" && targetItemObject is Door door)
                        {
                            //Här måste någon snajdig grej in för att returnera door-status till listan med doors.
                            targetItemObject.UnlockDoor();
                            FileHandler.SaveObjectToFile(targetItemObject);
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
        }
        else if (player.ActionStatus == "get")
        {
            (bool doesItemExist, string itemName) = Utilities.CheckForItemConnectedToAction(userInputAsArray);
            if (!doesItemExist)
            {
                Console.WriteLine("Error, the item you tried to get doesn't exist.");
            }
            else
            {
                room.SearchAllItemsInRoomBasedOnRoomNameAndUpdateListOfItemsInRoom(player.CurrentRoom.Name);
                var itemToUpdate = room.ItemsInRoom.FirstOrDefault(x => x.Name == itemName);

                if (itemToUpdate is not null && itemToUpdate.IsMovable)
                {
                    itemToUpdate.Room.Name = "On Person";
                    FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(itemToUpdate, itemToUpdate.Name);
                    Console.WriteLine("You got it on your person now.");
                }
                else if (!itemToUpdate.IsMovable)
                {
                    Console.WriteLine($"{itemToUpdate.Name} is not movable.");
                }
                else
                {
                    Console.WriteLine($"There is no way to interact with {itemName} in {player.CurrentRoom.Name}");

                }
            }
        }
        else if (player.ActionStatus == "drop")
        {
            (bool doesItemExist, string itemName) = Utilities.CheckForItemConnectedToAction(userInputAsArray);
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
        else if (player.ActionStatus == "search")
        {
            Console.WriteLine("The items in this room are:");
            foreach (var item in room.ItemsInRoom)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
        else if (player.ActionStatus == "inspect")
        {
            (bool doesItemExist, string itemName) = Utilities.CheckForItemConnectedToAction(userInputAsArray);
            var itemToInspect = room.ItemsInRoom.FirstOrDefault(x => x.Name == itemName);
            
            if (!doesItemExist)
            {
                (bool doesRoomExist, string roomName) = Utilities.CheckForTargetRoom(userInputAsArray);
                var roomToInspect = Room.Rooms.FirstOrDefault(room => room.Name == roomName);
                if (doesRoomExist && roomToInspect is not null)
                {
                    Console.WriteLine("You inspect the room:\n");
                    Console.WriteLine(roomToInspect.Description);
                }
                else
                {
                    Console.WriteLine($"{roomName}");
                }
            }
            else if (itemToInspect is not null)
            {
                Console.WriteLine("You inspect it:\n");
                Console.WriteLine(itemToInspect.Description);
            }
            else
            {
                Console.WriteLine("The item you try to inspect doesn't exist.");
            }
        }
        else
        {
            Console.WriteLine($"{player.ActionStatus}");
        }
        return (localKeepGameLoopGoing, player);
    }
}