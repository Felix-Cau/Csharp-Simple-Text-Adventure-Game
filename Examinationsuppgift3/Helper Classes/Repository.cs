using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class Repository
{
    public static List<Object> AllObjectsInGame { get; private set; } = new();
    // public static List<Door> AllDoorsInGame { get; private set; } = new List<Door>();
    // public static List<Room> AllRoomsInGame { get; private set; } = new List<Room>();
    //
    // public static List<Item> AllItemsOnPlayer { get; private set; } = new List<Item>();
    
    
    public static void LoadAllObjectsInGame()
    {
        AllObjectsInGame = FileHandler.ReadObjectsInFile<Object>().OfType<Object>().ToList();
    }

    // public static void LoadAllDoorsInGame()
    // {
    //     AllDoorsInGame = FileHandler.ReadObjectsInFile<Door>().OfType<Door>().ToList();
    // }
    //
    // public static void LoadAllRoomsInGame()
    // {
    //     AllRoomsInGame = FileHandler.ReadObjectsInFile<Room>().OfType<Room>().ToList();
    // }
    //
    // public static void LoadAllItemsOnPlayer()
    // {
    //     AllItemsOnPlayer = FileHandler.ReadObjectsInFile<Item>().OfType<Item>().Where(item => item.Room.Name == "On Person").ToList();
    // }
}