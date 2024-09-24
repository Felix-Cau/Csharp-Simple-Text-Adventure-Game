
using Examinationsuppgift3.Classes;
using Examinationsuppgift3.Helper_Classes;

bool keepGameGoing = true;

while (keepGameGoing)
{
    Console.WriteLine(Static_Messages.BeforeWelcome);
    var player = MenuHandler.CreatePlayer();
    
    bool keepGameLoopGoing = true;

    do
    {
        MenuHandler.DisplayMainMenu(player);
        var userInput = Console.ReadLine().ToLower().Trim();
        switch (userInput)
        {
            case "1":
                Console.Clear();
                Console.WriteLine(Static_Messages.WelcomeAndStart);
                Console.WriteLine(Static_Messages.AskUserForNextAction);
                var userInputAsArray = UserInputHandler.UserInputToArray();
                
                
                break;
            case "2":
                Console.WriteLine(Static_Messages.Goodbye);
                keepGameLoopGoing = false;
                keepGameGoing = false;
                break;
        }
    } while (keepGameLoopGoing);
}

