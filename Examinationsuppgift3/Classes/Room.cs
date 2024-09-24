using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Room : Entity, ISavable, ILoadable
{
    public List<Room> Rooms { get; private set; } = FileHandler.UnfilteredEntities.OfType<Room>().ToList();

    public static List<Item> ItemsInRoom { get; private set; } = SearchForAllItemsInRoomBasedOnRoomName("Bar");
    
    public Room(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public void SaveObjectToFile(Entity room)
    {
        FileHandler.SaveObjectToFile(room);
    }

    public static List<Item> SearchForAllItemsInRoomBasedOnRoomName(string roomName)
    {
        var localItemList = FileHandler.UnfilteredEntities.OfType<Item>().Where(item => item.Room.Name == roomName).ToList();
        
            //alternativ med Query syntax.
            // (from entity in FileHandler.UnfilteredEntities
            //                 where entity is Item item && item.Room.Name == roomName
            //                     select entity as Item).ToList();
            
        return localItemList;
    }
    public void LoadObject()
    {
        var localListOfRooms = FileHandler.ReadObjectsInFile().OfType<Room>().ToList();

        Rooms = localListOfRooms;
    }
}