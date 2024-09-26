using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class UserInputHandler
{
    public static string[] UserInputToArray()
    {
        var userInput = Console.ReadLine().Trim().ToLower().Split(" ");

        string[] fixedUserInputArray = new string[5];
        
        for (int i = 0; i < Math.Min(userInput.Length, 5); i++)
        {
            fixedUserInputArray[i] = userInput[i];
        }
        
        return fixedUserInputArray;
    }
    
    public static Player CheckForActionKeywords(Player player, string[] userInputAsArray)
    {
        switch (userInputAsArray[0])
        {
            case "use":
                player.SetActionStatus("use");
                break;
            case "get":
                player.SetActionStatus("get");
                break;
            case "drop":
                player.SetActionStatus("drop");
                break;
            case "search":
                player.SetActionStatus("search");
                break;
            case "inspect":
                player.SetActionStatus("inspect");
                break;
            default:
                player.SetActionStatus("Invalid command. Please try again.");
                break;
        }
        return player;
    }
}