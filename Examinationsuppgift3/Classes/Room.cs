using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;


namespace Examinationsuppgift3.Classes;

public class Room : Entity
{
    public static List<Room> Rooms { get; private set; } 
        // = FileHandler.ReadObjectsInFile<Room>().OfType<Room>().ToList();

    public List<Item> ItemsInRoom { get; private set; } 
        // = SetAllItemsInRoomBasedOnRoomNameOnStartup("Bar");
    
    public Room(string name, string description) : base(name, description)
    {
    }
    
    public void SetAllItemsInRoomOnStartup()
    {
        ItemsInRoom = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().Where(item => item.Room.Name == "Bar").ToList();
        
            //alternativ med Query syntax.
            // (from entity in FileHandler.UnfilteredEntities
            //                 where entity is Item item && item.Room.Name == roomName
            //                     select entity as Item).ToList();
            
    }
    
    public void SearchAllItemsInRoomBasedOnRoomNameAndUpdateListOfItemsInRoom(string roomName)
    {
        var localItemList = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().Where(item => item.Room.Name == roomName).ToList();
        ItemsInRoom = localItemList;
    }
}