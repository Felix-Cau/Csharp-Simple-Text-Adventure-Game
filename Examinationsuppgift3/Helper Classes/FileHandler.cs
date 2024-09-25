using System.Text.Json;
using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class FileHandler
{
    private static readonly string _filePath = "c:/dev/Skola/CsharpKurs1/DbFiles/GameDb.json";
    
    private static string _temporaryObjectAsJson = string.Empty;

    private static StreamWriter _writer;
    private static StreamReader _reader;
    
    public static List<T> ReadObjectsInFile<T>()
    {
        var items = new List<T>();
        using (_reader = new StreamReader(_filePath))
        {
            while (!_reader.EndOfStream)
            {
                var objectAsString = _reader.ReadLine();
                T item = JsonSerializer.Deserialize<T>(objectAsString);
                items.Add(item);
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
        _temporaryObjectAsJson = FindObjectJsonStringInDbFile<T>(entityName);
        
        string fileContent = File.ReadAllText(_filePath);
        fileContent = fileContent.Replace(_temporaryObjectAsJson, entityName);
        File.WriteAllText(_filePath, fileContent);
    
        _temporaryObjectAsJson = string.Empty;
    }

    public static string FindObjectJsonStringInDbFile<T>(string inputObjectName)
    {
        var itemToOverwriteAsJsonString = JsonSerializer.Serialize(ReadObjectsInFile<T>().FirstOrDefault(x => (x as dynamic).Name.Equals(inputObjectName)));
        return itemToOverwriteAsJsonString;
    }
}