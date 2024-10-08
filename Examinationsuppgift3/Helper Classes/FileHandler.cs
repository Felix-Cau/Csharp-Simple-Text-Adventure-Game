using System.ComponentModel;
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
                            var doorFromJsonString = JsonSerializer.Deserialize<Door>(objectAsString);
                            items.Add(doorFromJsonString);
                            break;
                        }
                        case "Room":
                        {
                            var roomFromJsonString = JsonSerializer.Deserialize<Room>(objectAsString);
                            items.Add(roomFromJsonString);
                            break;
                        }
                        case "Item":
                        {
                            var itemFromJsonString = JsonSerializer.Deserialize<Item>(objectAsString);
                            items.Add(itemFromJsonString);
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
    
    public static void SaveObjectsToResetGame<T>(T obj)
    {
        _temporaryObjectAsJson = JsonSerializer.Serialize(obj);

        using (_writer = new StreamWriter(_filePath, true))
        {
            _writer.WriteLine(_temporaryObjectAsJson);
        }
        _temporaryObjectAsJson = string.Empty;
    }
    
    public static void ClearJsonFile()
    {
        File.WriteAllText(_filePath, string.Empty);
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

    public static void ResetGame()
    {
        ClearJsonFile();
        
        Room Bar = new Room("Bar", "The old worn-down bar has seen better days, with splintered wooden beams and a dim, flickering\n" +
                           "overhead light that barely illuminates the grime. The bar top is a patchwork of scars and stains, its once-polished\n" +
                           "surface now dull and sticky from years of spilled drinks. Stools with torn, faded upholstery wobble precariously,\n" +
                           "and the air is thick with the pungent mix of cheap beer and stale smoke. Crooked posters cling to the walls, peeling\n" +
                           "at the edges, while the floorboards creak ominously underfoot. A jukebox in the corner sputters out scratchy tunes,\n" +
                           "its faded selection a testament to a bygone era. This place feels alive with the echoes of raucous laughter and heated\n" +
                           "arguments, a rough-and-tumble refuge for those seeking a cold drink and a slice of history.");
        Room Hallway = new Room("Hallway", "The hallway is a dimly lit passageway, its worn carpet muffling footsteps as you walk through.\n" +
                                   "The walls, lined with peeling wallpaper, tell stories of time gone by. A coat rack leans slightly, burdened\n" +
                                   "with a collection of mismatched jackets and a single, battered umbrella hanging limply. The chandelier above\n" +
                                   "casts a feeble glow, its crystals dust-covered and dull, swaying gently with the slightest breeze. A mirror hangs\n" +
                                   "at the end of the hallway, its surface foggy and cracked, reflecting a distorted image of the space. Shadows dance\n" +
                                   "in the corners, creating an air of mystery, as if the hallway itself has secrets to share from years of forgotten\n" +
                                   "comings and goings.");
        Room OnPerson = new Room("On Person", "What you have in your pockets.");
        Room DarkEndRoom = new Room("Dark End Room",
                    "The dark room envelops you in an unsettling silence, the air thick and heavy, as if holding its breath.\n" +
                            "Shadows loom ominously, merging into the corners where light dares not tread. The faintest hint of cold seeps in,\n" +
                            "raising goosebumps on your skin. As your eyes strain to adjust, you can barely make out the outline of another door,\n" +
                            "its edges blurred and indistinct against the blackness. The door stands there like a silent sentinel, a threshold to\n" +
                            "the unknown, inviting yet foreboding. A chill runs down your spine as you sense the weight of the darkness pressing in,\n" +
                            "and the silence seems to pulse with unseen energy, leaving you acutely aware that you are not alone in this forgotten space.\n" +
                            "Congratulations, you have finished the game. Press any key to exit the game.");
        Room Storage = new Room("Storage", "This is purely to keep it separated from the game");

        Item Bottle = new Item("Bottle", "An old bottle of whisky.", true, Bar);
        Item OpenedBottle = new Item("OpenedBottle", "The bottle of opened whisky. It smells delightful!", true, Storage);
        Item Corkscrew = new Item("Corkscrew", "A shiny new and super sharp corkscrew.", true, Bar);
        Item Table = new Item("Table", "A sturdy wooden table with carvings and blood stains.", false, Bar);
        Item Chair = new Item("Chair", "A rickety bar stool.", false, Bar);
        Item Glass = new Item("Glass", "An empty old beer glass.", true, Bar);
        Item Key = new Item("Key", "An old key.", true, Hallway);
        Item Painting = new Item("Painting", "An old painting of what seems like a sailor. And what you think might be fresh blood stains and brain matter.", false, Bar);
        Item CoatRack = new Item("CoatRack", "An old coat rack.", false, Hallway);
        Item Umbrella = new Item("Umbrella", "A black umbrella that is still wet.", true, Hallway);
        Item Carpet = new Item("Carpet", "A long, red carpet that runs the length of the hallway.", false, Hallway);
        Item Mirror = new Item("Mirror", "A large mirror reflecting the dimly lit hallway and your own grim face.", false, Hallway);
        Item Chandelier = new Item("Chandelier", "An old chandelier hanging from the ceiling, partly broken. And wierdly a human hand hanging among the crystals.", false, Hallway);
        Item Lighter = new Item("Lighter", "A small, refillable metal lighter.", true, OnPerson);
        Item Knife = new Item("Knife", "A compact and foldable knife.", true, OnPerson);
        Item EnergyBar = new Item("Energy Bar", "A small energy bar with peanuts and other random perservatives.", true, OnPerson);

        List<Room> rooms =
        [
            Bar,
            Hallway,
            OnPerson,
            DarkEndRoom,
            Storage
        ];

        List<Item> itemsInGame =
        [
            Bottle,
            OpenedBottle,
            Corkscrew,
            Table,
            Chair,
            Glass,
            Key,
            Painting,
            CoatRack,
            Umbrella,
            Carpet,
            Mirror,
            Chandelier,
            Lighter,
            Knife,
            EnergyBar
        ];

        Door DoorFromTheBar = new Door("DoorFromTheBar", "A cold looking metal door", false, Bar, false);
        Door ToTheBar =  new Door("DoorBackToTheBar", "A cold looking metal door", false, Hallway, false);
        Door MysteriousDoor = new Door("MysteriousDoor",
            "You can't see much beyond the outline of the door because of the darkness. But as you feel it, it is almost as someone has carved a shallow pattern on it.",
            false, Hallway, true);

        List<Door> DoorsInGameList =
        [
            DoorFromTheBar,
            ToTheBar,
            MysteriousDoor
        ];
       

        foreach (Room room in rooms)
        {
            SaveObjectsToResetGame(room);
        }

        foreach (Item item in itemsInGame)
        {
            SaveObjectsToResetGame(item);
        }

        foreach (Door door in DoorsInGameList)
        {
            SaveObjectsToResetGame(door);
        }

        Repository.LoadAllObjectsInGame();
    }
}
    