using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class Repository
{
    public static List<Object> AllObjectsInGame { get; private set; } = new();
    
    public static void LoadAllObjectsInGame()
    {
        AllObjectsInGame = FileHandler.ReadObjectsInFile().OfType<Object>().ToList();
    }
}