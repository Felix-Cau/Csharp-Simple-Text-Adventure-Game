namespace Examinationsuppgift3.Helper_Classes;

public static class UserInputHandler
{
    public static string[] UserInputToArray()
    {
        var userInput = Console.ReadLine().Trim().ToLower().Split(" ");
        return userInput;
    }
}