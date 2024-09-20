using System.Text.Json;

namespace Examinationsuppgift3.Helper_Classes;

public static class FileHandler
{
    private const string _filePath = @"c:/dev/Skola/CsharpKurs1/DbFiles/GameDb.json";
    
    private static string _temporaryObjectAsJson = string.Empty;

    public static List<T> ReadObjectsInFile<T>(T obj)
    {
        List<T> items = new();
        using (StreamReader reader = new StreamReader(_filePath))
        {
            while (!reader.EndOfStream)
            {
                string objectAsString = reader.ReadLine();
                T item = JsonSerializer.Deserialize<T>(objectAsString);
                items.Add(item);
            }
        }
        return items;
    }

    public static void SaveObjectToFile<T>(T obj)
    {
        string objectAsJsonString = JsonSerializer.Serialize(obj);

        using (StreamWriter writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine(objectAsJsonString);
        }
        
    }

    public static void RemoveObjectFromList<T>(T obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);
        
        List<string> jsonStringLines = File.ReadAllLines(_filePath).ToList();
        jsonStringLines.RemoveAll(line => line.Equals(_temporaryObjectAsJson));
        File.WriteAllLines(_filePath, jsonStringLines);
    }
}