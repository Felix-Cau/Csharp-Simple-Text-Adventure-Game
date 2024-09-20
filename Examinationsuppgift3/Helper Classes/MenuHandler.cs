using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class MenuHandler
{
    private static List<string> MainMenu = new()
    {
        "You have entered a text adventure game in the Zombie apocalypse",
        "Navigate in the menu by simply writing the number of the option you wish to do",
        "1. New game",
        "2. Exit game"
    };
    public static void DisplayMainMenu()
    {
        Console.Clear();
        foreach (string line in MainMenu)
        {
            Console.WriteLine(line);
        } 
    }

    public static Player CreatePlayer(Player player)
    {
        Console.WriteLine("Please enter the name of your carachter: ");
        player.Name = Console.ReadLine().Trim();
        Console.WriteLine("Please enter a short desciption of you carachter: ");
        player.Description = Console.ReadLine().Trim();        
        
        return player;
    }
}