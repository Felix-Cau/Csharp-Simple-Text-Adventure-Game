using Examinationsuppgift3.Classes;

namespace Examinationsuppgift3.Helper_Classes;

public static class MenuHandler
{
    private static List<string> menuItems =
    [
        "You have entered a text adventure game in the Zombie apocalypse",
        "Navigate in the menu by simply writing the number of the option you wish to do",
        "1. New game",
        "2. Exit game",
        "3. Reset all game files"
    ];
    
    public static void DisplayMainMenu(Player player)
    {
        Console.Clear();
        Console.WriteLine($"Welcome {player.Name}!");
        foreach (string str in menuItems)
        {
            Console.WriteLine(str);
        } 
    }

    public static Player CreatePlayer()
    {
        var name = string.Empty;
        var description = string.Empty;
        
        Console.WriteLine(Static_Messages.AskUserToNamePlayer);
        name = Console.ReadLine();
        Console.WriteLine(Static_Messages.AskUserForShortPlayerDescription);
        description = Console.ReadLine();        
        
        Player player = new Player(name, description);
        
        return player;
    }
}