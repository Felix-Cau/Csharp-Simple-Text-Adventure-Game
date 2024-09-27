
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
                
                FileHandler.ResetGame();
                
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
            case "3":
                FileHandler.ResetGame();
                Console.WriteLine("You have successfully reset the game files. Press any key to continue.");
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Invalid input. Try again.");
                break;
        }
    } while (keepMenuLoopGoing);
}