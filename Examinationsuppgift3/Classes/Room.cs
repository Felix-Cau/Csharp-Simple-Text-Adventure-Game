using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Room : Entity, ISavable
{
    public static List<Room> Rooms { get; private set; } = FileHandler.UnfilteredEntities<Room>().OfType<Room>().ToList();

    public List<Item> ItemsInRoom { get; private set; } = SetAllItemsInRoomBasedOnRoomNameOnStartup("Bar");
    
    public Room(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }
    private static List<Item> SetAllItemsInRoomBasedOnRoomNameOnStartup(string roomName)
    {
        var localItemList = FileHandler.UnfilteredEntities<Item>().OfType<Item>().Where(item => item.Room.Name == roomName).ToList();
        
            //alternativ med Query syntax.
            // (from entity in FileHandler.UnfilteredEntities
            //                 where entity is Item item && item.Room.Name == roomName
            //                     select entity as Item).ToList();
            
        return localItemList;
    }
    public void SearchAllItemsInRoomBasedOnRoomNameAndUpdateListOfItemsInRoom(string roomName)
    {
        var localItemList = FileHandler.UnfilteredEntities<Item>().OfType<Item>().Where(item => item.Room.Name == roomName).ToList();
        ItemsInRoom = localItemList;
    }
    public void SaveObjectToFile<T>(T room)
    {
        FileHandler.SaveObjectToFile(room);
    }
}