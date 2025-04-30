using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Check if we're running tests
                if (args.Length > 0 && args[0].ToLower() == "test")
                {
                    Testing.RunTests();
                    return;
                }

                Console.WriteLine("Welcome to DUNGEON EXPLORER");
                Console.WriteLine("===================================");
                Console.WriteLine("This is a text-based adventure game");
                Console.WriteLine();

                // Create and start the game
                Game game = new Game();
                game.Start();
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                // This will always execute, even if there's an error
                Console.WriteLine("\nGame over. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}