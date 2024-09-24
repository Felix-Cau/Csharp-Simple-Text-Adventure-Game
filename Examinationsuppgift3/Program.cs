
using Examinationsuppgift3.Classes;
using Examinationsuppgift3.Helper_Classes;

bool keepGameGoing = true;

while (keepGameGoing)
{
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

                while (keepGameLoopGoing)
                {
                    Console.WriteLine(Static_Messages.AskUserForNextAction);
                    var userInputAsArray = UserInputHandler.UserInputToArray();
                    (keepGameLoopGoing, player) = EventResolver.ResolveEvents(player, userInputAsArray);
                }
                break;
            case "2":
                Console.WriteLine(Static_Messages.Goodbye);
                keepMenuLoopGoing = false;
                keepGameGoing = false;
                break;
            default:
                Console.WriteLine("Invalid input. Try again.");
                break;
        }
    } while (keepMenuLoopGoing);
}

