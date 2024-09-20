namespace Examinationsuppgift3.Helper_Classes;

public static class UserInputHandler
{
    public static string[] AskForUserInput()
    {
        Console.WriteLine("What do you do now?");
        var userInput = Console.ReadLine().Trim().ToLower().Split(" ");
        return userInput;
    }
}