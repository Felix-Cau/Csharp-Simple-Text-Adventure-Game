using System.Reflection.Emit;
using Examinationsuppgift3.Helper_Classes;
using Examinationsuppgift3.Interfaces;

namespace Examinationsuppgift3.Classes;

public class Room : Entity, ISavable, ILoadable
{
    public static List<Room> Rooms { get; private set; } = FileHandler.UnfilteredEntities.OfType<Room>().ToList();

    // public static List<Item> ItemsInRoom { get; private set; } 
        // =
        // FileHandler.UnfilteredEntities.OfType<Item>().OrderBy(item => item.Room.Name).ToList();
    
    public Room(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public void SaveObjectToFile<Room>(Room room)
    {
        FileHandler.SaveObjectToFile(room);
    }

    // public static List<Item> SearchForAllItemsInRoomBasedOnRoomName(string roomName)
    // {
    //     return FileHandler.UnfilteredEntities.OfType<Item>()
    //         .Where(item => item.Room.Name.ToLower() == roomName.ToLower()).ToList();
    // }
    public List<Room> LoadObject<Room>() where Room : class
    {
        var localListOfRooms = FileHandler.ReadObjectsInFile<Entity>().OfType<Room>().ToList();

        return localListOfRooms;
    }
}