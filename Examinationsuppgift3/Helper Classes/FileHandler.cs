using System.Text.Json;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class FileHandler
{
    private const string _filePath = @"c:/dev/Skola/CsharpKurs1/DbFiles/GameDb.json";
    
    private static string _temporaryObjectAsJson = string.Empty;

    private static StreamWriter _writer;
    private static StreamReader _reader;
    
    public static List<Entity> UnfilteredEntities = ReadObjectsInFile().ToList();
    
    public static List<Entity> ReadObjectsInFile()
    {
        var items = new List<Entity>();
        using (_reader = new StreamReader(_filePath))
        {
            while (!_reader.EndOfStream)
            {
                var objectAsString = _reader.ReadLine();
                Entity item = JsonSerializer.Deserialize<Entity>(objectAsString);
                items.Add(item);
            }
        }
        return items;
    }

    public static void SaveObjectToFile(Entity obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);

        using (_writer = new StreamWriter(_filePath, true))
        {
            _writer.WriteLine(_temporaryObjectAsJson);
        }
        _temporaryObjectAsJson = string.Empty;
        
        //Uppdaterar UnfilteredEntities-listan.
        UnfilteredEntities = ReadObjectsInFile().ToList();
    }

    public static void RemoveObjectFromFile(Entity obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);
        
        var jsonStringLines = File.ReadAllLines(_filePath).ToList();
        jsonStringLines.RemoveAll(line => line.Equals(_temporaryObjectAsJson));
        File.WriteAllLines(_filePath, jsonStringLines);
        
        _temporaryObjectAsJson = string.Empty;
        
        //Uppdaterar UnfilteredEntities-listan.
        UnfilteredEntities = ReadObjectsInFile().ToList();
    }

    public static void OverwriteObjectFromFileAndChangeObjectDetails(Entity objToUpdate, string entityName)
    {
        var inputObjectAsJsonString = JsonSerializer.Serialize(objToUpdate);
        _temporaryObjectAsJson = FindObjectJsonStringInDbFile(entityName);
        
        string fileContent = File.ReadAllText(_filePath);
        fileContent = fileContent.Replace(_temporaryObjectAsJson, entityName);
        File.WriteAllText(_filePath, fileContent);

        _temporaryObjectAsJson = string.Empty;
        
        //Uppdaterar UnfilteredEntities-listan.
        UnfilteredEntities = ReadObjectsInFile().ToList();
    }

    public static string FindObjectJsonStringInDbFile(string input)
    {
        var itemToOverwriteAsJsonString = JsonSerializer.Serialize(UnfilteredEntities.FirstOrDefault(x => x.Name.Equals(input)));
        return itemToOverwriteAsJsonString;
    }
}