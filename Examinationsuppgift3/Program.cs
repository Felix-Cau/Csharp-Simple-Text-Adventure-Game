
using Examinationsuppgift3.Classes;
using Examinationsuppgift3.Helper_Classes;


bool keepGoing = true;


while (keepGoing)
{
    MenuHandler.DisplayMainMenu();
    var userInput = Console.ReadLine().Trim();
    switch (userInput)
    {
        case "1":
            Console.Clear();
            Console.WriteLine(Static_Messages.WelcomeAndStart);
            Console.WriteLine();
            break;
        case "2":
            Console.WriteLine(Static_Messages.Goodbye);
            keepGoing = false;
            break;
    }
}