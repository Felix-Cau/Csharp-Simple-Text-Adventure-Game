using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class FileHandler
{
    private static readonly string _filePath = "c:/dev/Skola/CsharpKurs1/DbFiles/GameDb.json";
    
    private static string _temporaryObjectAsJson = string.Empty;
    
    

    private static StreamWriter _writer;
    private static StreamReader _reader;
    
    public static List<Object> ReadObjectsInFile()
    {
        var items = new List<Object>();
        using (_reader = new StreamReader(_filePath))
        {
            while (!_reader.EndOfStream)
            {
                var objectAsString = _reader.ReadLine();
                var jsonDocument = JsonDocument.Parse(objectAsString).RootElement;
                
                if (jsonDocument.TryGetProperty("ObjectType", out var objectsTypeElement))
                {
                    var objectsType = objectsTypeElement.GetString();

                    switch (objectsType)
                    {
                        case "Door":
                        {
                            var doorAsJsonstring = JsonSerializer.Deserialize<Door>(objectAsString);
                            items.Add(doorAsJsonstring);
                            break;
                        }
                        case "Room":
                        {
                            var roomAsJsonstring = JsonSerializer.Deserialize<Room>(objectAsString);
                            items.Add(roomAsJsonstring);
                            break;
                        }
                        case "Item":
                        {
                            var itemAsJsonstring = JsonSerializer.Deserialize<Item>(objectAsString);
                            items.Add(itemAsJsonstring);
                            break;
                        }
                    }
                }
            }
        }
        return items;
    }
    
    public static void SaveObjectToFile<T>(T obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);

        using (_writer = new StreamWriter(_filePath, true))
        {
            _writer.WriteLine(_temporaryObjectAsJson);
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

    public static void OverwriteObjectFromFileAndChangeObjectDetails<T>(T objToUpdate, string entityName)
    {
        var inputObjectAsJsonString = JsonSerializer.Serialize(objToUpdate);
        _temporaryObjectAsJson = FindObjectJsonStringInDbFile(entityName);
        
        List<string> fileContent = File.ReadAllLines(_filePath).ToList();
        fileContent.Remove(_temporaryObjectAsJson);
        fileContent.Add(inputObjectAsJsonString);
        File.WriteAllLines(_filePath, fileContent);
    
        _temporaryObjectAsJson = string.Empty;
    }

    public static string FindObjectJsonStringInDbFile(string inputObjectName)
    {
        var itemToOverwriteAsJsonString = JsonSerializer.Serialize(ReadObjectsInFile().FirstOrDefault(x => (x as dynamic).Name.Equals(inputObjectName)));
        return itemToOverwriteAsJsonString;
    }
}