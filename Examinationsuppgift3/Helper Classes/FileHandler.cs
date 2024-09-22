using System.Text.Json;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class FileHandler
{
    private const string _filePath = @"c:/dev/Skola/CsharpKurs1/DbFiles/GameDb.json";
    
    private static string _temporaryObjectAsJson = string.Empty;
    
    public static List<Entity> UnfilteredEntities = ReadObjectsInFile<Entity>().ToList();
    
    public static List<T> ReadObjectsInFile<T>()
    {
        var items = new List<T>();
        using (StreamReader reader = new StreamReader(_filePath))
        {
            while (!reader.EndOfStream)
            {
                var objectAsString = reader.ReadLine();
                T item = JsonSerializer.Deserialize<T>(objectAsString);
                items.Add(item);
            }
        }
        return items;
    }

    public static void SaveObjectToFile<T>(T obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);

        using (StreamWriter writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine(_temporaryObjectAsJson);
        }
        _temporaryObjectAsJson = string.Empty;
    }

    public static void RemoveObjectFromFile<T>(T obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);
        
        var jsonStringLines = File.ReadAllLines(_filePath).ToList();
        jsonStringLines.RemoveAll(line => line.Equals(_temporaryObjectAsJson));
        File.WriteAllLines(_filePath, jsonStringLines);
        
        _temporaryObjectAsJson = string.Empty;
    }
}