
using Examinationsuppgift3.Classes;
using Examinationsuppgift3.Helper_Classes;

bool keepGameGoing = true;

while (keepGameGoing)
{
    Repository.LoadAllObjectsInGame();
    
    Console.WriteLine(Static_Messages.BeforeWelcome);
    var player = MenuHandler.CreatePlayer();
    
    bool keepMenuLoopGoing = true;

    do
    {
        MenuHandler.DisplayMainMenu(player);
        var userInput = Console.ReadLine().ToLower().Trim();
        switch (userInput)
        {
            case "1":
                Console.Clear();
                Console.WriteLine(Static_Messages.WelcomeAndStart);
                
                bool keepGameLoopGoing = true;
                
                Room room = Repository.AllObjectsInGame.OfType<Room>().Where(room => room.Name == player.CurrentRoom.Name).FirstOrDefault();

                while (keepGameLoopGoing)
                {
                    Console.WriteLine(Static_Messages.AskUserForNextAction);
                    var userInputAsArray = UserInputHandler.UserInputToArray();
                    (keepGameLoopGoing, player, room) = EventResolver.ResolveEvents(player, userInputAsArray);
                }
                break;
            case "2":
                Console.WriteLine(Static_Messages.Goodbye);
                keepMenuLoopGoing = false;
                keepGameGoing = false;
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Invalid input. Try again.");
                break;
        }
    } while (keepMenuLoopGoing);
}
//
// using Examinationsuppgift3.Classes;
// using Examinationsuppgift3.Helper_Classes;
//
// Room Bar = new Room("Bar", "The old worn-down bar has seen better days, with splintered wooden beams and a dim, flickering\n" +
//                            "overhead light that barely illuminates the grime. The bar top is a patchwork of scars and stains, its once-polished\n" +
//                            "surface now dull and sticky from years of spilled drinks. Stools with torn, faded upholstery wobble precariously,\n" +
//                            "and the air is thick with the pungent mix of cheap beer and stale smoke. Crooked posters cling to the walls, peeling\n" +
//                            "at the edges, while the floorboards creak ominously underfoot. A jukebox in the corner sputters out scratchy tunes,\n" +
//                            "its faded selection a testament to a bygone era. This place feels alive with the echoes of raucous laughter and heated\n" +
//                            "arguments, a rough-and-tumble refuge for those seeking a cold drink and a slice of history.");
// Room Hallway = new Room("Hallway", "The hallway is a dimly lit passageway, its worn carpet muffling footsteps as you walk through.\n" +
//                                    "The walls, lined with peeling wallpaper, tell stories of time gone by. A coat rack leans slightly, burdened\n" +
//                                    "with a collection of mismatched jackets and a single, battered umbrella hanging limply. The chandelier above\n" +
//                                    "casts a feeble glow, its crystals dust-covered and dull, swaying gently with the slightest breeze. A mirror hangs\n" +
//                                    "at the end of the hallway, its surface foggy and cracked, reflecting a distorted image of the space. Shadows dance\n" +
//                                    "in the corners, creating an air of mystery, as if the hallway itself has secrets to share from years of forgotten\n" +
//                                    "comings and goings.");
// Room OnPerson = new Room("On Person", "What you have in your pockets.");
// Room DarkEndRoom = new Room("Dark End Room",
//     "The dark room envelops you in an unsettling silence, the air thick and heavy, as if holding its breath.\n" +
//     "Shadows loom ominously, merging into the corners where light dares not tread. The faintest hint of cold seeps in,\n" +
//     "raising goosebumps on your skin. As your eyes strain to adjust, you can barely make out the outline of another door,\n" +
//     "its edges blurred and indistinct against the blackness. The door stands there like a silent sentinel, a threshold to\n" +
//     "the unknown, inviting yet foreboding. A chill runs down your spine as you sense the weight of the darkness pressing in,\n" +
//     "and the silence seems to pulse with unseen energy, leaving you acutely aware that you are not alone in this forgotten space.\n" +
//     "Congratulations, you have finished the game. Press any key to exit the game.");
//
// Item Bottle = new Item("Bottle", "An old, empty bottle of whisky.", true, Bar);
// Item Table = new Item("Table", "A sturdy wooden table with carvings and blood stains.", false, Bar);
// Item Chair = new Item("Chair", "A rickety bar stool.", false, Bar);
// Item Glass = new Item("Glass", "An empty old beer glass.", true, Bar);
// Item Key = new Item("Key", "An old key.", true, Hallway);
// Item Painting = new Item("Painting", "An old painting of what seems like a sailor. And what you think might be fresh blood stains and brain matter.", false, Bar);
// Item CoatRack = new Item("CoatRack", "An old coat rack.", false, Hallway);
// Item Umbrella = new Item("Umbrella", "A black umbrella that is still wet.", true, Hallway);
// Item Carpet = new Item("Carpet", "A long, red carpet that runs the length of the hallway.", false, Hallway);
// Item Mirror = new Item("Mirror", "A large mirror reflecting the dimly lit hallway and your own grim face.", false, Hallway);
// Item Chandelier = new Item("Chandelier", "An old chandelier hanging from the ceiling, partly broken. And wierdly a human hand hanging among the crystals.", false, Hallway);
// Item Lighter = new Item("Lighter", "A small, refillable metal lighter.", true, OnPerson);
// Item Knife = new Item("Knife", "A compact and foldable knife.", true, OnPerson);
// Item EnergyBar = new Item("Energy Bar", "A small energy bar with peanuts and other random perservatives.", true, OnPerson);
//
// List<Room> rooms = new List<Room>();
// rooms.Add(Bar);
// rooms.Add(Hallway);
// rooms.Add(OnPerson);
// rooms.Add(DarkEndRoom);
//
// List<Item> itemsInGame = new List<Item>();
// itemsInGame.Add(Bottle);
// itemsInGame.Add(Table);
// itemsInGame.Add(Chair);
// itemsInGame.Add(Glass);
// itemsInGame.Add(Key);
// itemsInGame.Add(Painting);
// itemsInGame.Add(CoatRack);
// itemsInGame.Add(Umbrella);
// itemsInGame.Add(Carpet);
// itemsInGame.Add(Mirror);
// itemsInGame.Add(Chandelier);
// itemsInGame.Add(Lighter);
// itemsInGame.Add(Knife);
// itemsInGame.Add(EnergyBar);
//
// Door DoorFromTheBar = new Door("DoorFromTheBar", "A cold looking metal door", false, Bar, false);
// Door ToTheBar =  new Door("DoorBackToTheBar", "A cold looking metal door", false, Hallway, false);
// Door MysteriousDoor = new Door("MysteriousDoor",
//     "You can't see much beyond the outline of the door because of the darkness. But as you feel it, it is almost as someone has carved a shallow pattern on it.",
//     false, Hallway, true);
//
// List<Door> DoorsInGameList = new List<Door>();
// DoorsInGameList.Add(DoorFromTheBar);
// DoorsInGameList.Add(ToTheBar);
// DoorsInGameList.Add(MysteriousDoor);
//
// foreach (Room room in rooms)
// {
//     FileHandler.SaveObjectToFile(room);
// }
//
// Console.WriteLine("Rooms saved.");
//
// foreach (Item item in itemsInGame)
// {
//     FileHandler.SaveObjectToFile(item);
// }
//
// Console.WriteLine("Items saved.");
//
// foreach (Door door in DoorsInGameList)
// {
//     FileHandler.SaveObjectToFile(door);
// }
//
// Console.WriteLine("Doors saved.");