using System.ComponentModel.Design;
using System.Diagnostics;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class EventResolver
{
    public static (bool, Player, Room) ResolveEvents(Player inputPlayer, string[] userInputAsArray)
    {
        bool localKeepGameLoopGoing = true;
        var player = UserInputHandler.CheckForActionKeywords(inputPlayer, userInputAsArray);
        // var room = inputRoom;
        var room = Repository.AllObjectsInGame.OfType<Room>().Where(room => room.Name == player.CurrentRoom.Name).SingleOrDefault();

        if (player.ActionStatus == "use")
        {
            (bool doesDoorExist, string doorNameInLowerCase) = Utilities.CheckForDoorConnectedToAction(userInputAsArray);
            
            if (doesDoorExist)
            {
                switch (doorNameInLowerCase)
                {
                    case "doorfromthebar":
                        var localUpdatePlayerCurrentRoomWithHallway = Repository.AllObjectsInGame.OfType<Room>()
                            .SingleOrDefault(room => room.Name == "Hallway");
                        player.ChangeCurrentRoom(localUpdatePlayerCurrentRoomWithHallway);
                        Console.WriteLine("You have entered the hallway.");
                        break;
                    case "doorbacktothebar":
                        var localUpdatePlayerCurrentRoomWithBar = Repository.AllObjectsInGame.OfType<Room>()
                                                                  .SingleOrDefault(room => room.Name == "Bar");
                        player.ChangeCurrentRoom(localUpdatePlayerCurrentRoomWithBar);
                        Console.WriteLine("You have gone back to the bar.");
                        break;
                    case "mysteriousdoor":
                        var localUpdatePlayerCurrentRoomWithEndRoom = Repository.AllObjectsInGame.OfType<Room>()
                                                                      .SingleOrDefault(room => room.Name == "Dark End Room");
                        var localDarkEndRoomObject = Repository.AllObjectsInGame.OfType<Door>()
                                                     .SingleOrDefault(door => door.Name == "MysteriousDoor");

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
                    var targetItemObject = Repository.AllObjectsInGame.OfType<Door>().SingleOrDefault(door => door.Name == targetItemName);
                    
                    if (!doesTargetItemExist)
                    {
                        Console.WriteLine("Error, the target item you tried to use doesn't exist.");
                    }
                    else
                    {
                        
                        if (itemName.ToLower() == "key" && targetItemObject is not null)
                        {
                            if (targetItemObject.IsLocked)
                            {
                                targetItemObject.UnlockDoor();
                                FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(targetItemObject, targetItemObject.Name);
                                Repository.LoadAllObjectsInGame();
                                Console.WriteLine("You have unlocked the door.");
                            }
                            else if (!targetItemObject.IsLocked)
                            {
                                targetItemObject.LockDoor();
                                FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(targetItemObject, targetItemObject.Name);
                                Repository.LoadAllObjectsInGame();
                                Console.WriteLine("You have locked the door.");
                            }
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
                var localAllItemsInRoom = Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == player.CurrentRoom.Name).ToList();
                var itemToUpdate = localAllItemsInRoom.FirstOrDefault(x => x.Name == itemName);

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
                var itemToUpdate = Repository.AllObjectsInGame.OfType<Item>().FirstOrDefault(x => x.Name == itemName);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Room.Name = room.Name;
                    FileHandler.OverwriteObjectFromFileAndChangeObjectDetails(itemToUpdate, itemToUpdate.Name);
                    Repository.LoadAllObjectsInGame();
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
            (bool isItThePlayer, string onPlayerName) = Utilities.CheckIfTargetIsPlayer(userInputAsArray);

            if (isItThePlayer)
            {
                var localPlayerInventory = Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == onPlayerName).ToList();
                Console.WriteLine("The item you carry are:");
                foreach (var item in localPlayerInventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            else
            {
                var localRoomAndItemList = Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == player.CurrentRoom.Name).ToList();
                Console.WriteLine("The items in this room are:");
                foreach (var item in localRoomAndItemList)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }
        }
        else if (player.ActionStatus == "inspect")
        {
            (bool doesItemExist, string itemName) = Utilities.CheckForItemConnectedToAction(userInputAsArray);
            var localAllItemsInRoom = Repository.AllObjectsInGame.OfType<Item>().Where(item => item.Room.Name == player.CurrentRoom.Name).ToList();

            var itemToInspect = localAllItemsInRoom.FirstOrDefault(x => x.Name == itemName);
            
            if (!doesItemExist)
            {
                (bool doesRoomExist, string roomName) = Utilities.CheckForTargetRoom(userInputAsArray);
                var roomToInspect = Repository.AllObjectsInGame.OfType<Room>().FirstOrDefault(room => room.Name == roomName);
                if (doesRoomExist && roomToInspect is not null)
                {
                    Console.WriteLine("You inspect the room:\n");
                    Console.WriteLine(roomToInspect.Description);
                }
                else
                {
                    Console.WriteLine("Something went wrong. Try to inspect again.");
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
        return (localKeepGameLoopGoing, player, room);
    }
}