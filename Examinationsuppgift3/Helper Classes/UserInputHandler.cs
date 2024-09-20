namespace Examinationsuppgift3.Helper_Classes;

public static class UserInputHandler
{
    public static string[] AskForUserInput()
    {
        Console.WriteLine(Static_Messages.AskUserForNextAction);
        var userInput = Console.ReadLine().Trim().ToLower().Split(" ");
        return userInput;
    }
}