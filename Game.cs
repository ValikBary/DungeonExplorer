using System;
using System.Media;
using System.Linq;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        
        public Game()
        {
            // Initialize the game with one room and one player
            try
            {
                // Create a starting room with an item
                currentRoom = new Room("You are in a dark, damp dungeon room. There is a door to the north.", "key");

                // Ask for player name and create player
                Console.WriteLine("Welcome to Dungeon Explorer!");
                Console.Write("Enter your character's name: ");
                string playerName = Console.ReadLine();

                // Create player with initial health of 100
                player = new Player(playerName, 100);
                Console.WriteLine($"Welcome, {player.Name}! Your adventure begins...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing game: {ex.Message}");
                
            }
        }

        public void Start()
        {
            // Set playing to true to start the game loop
            bool playing = true;

            
            Console.WriteLine(currentRoom.GetFullDescription());

            while (playing)
            {
                try
                {
                    // Display prompt and get player command
                    Console.Write("\nWhat would you like to do? (look, status, take, quit): ");
                    string input = Console.ReadLine().Trim();

                    // Convert to lowercase for case-insensitive comparison
                    string command = input.ToLower();

                    // List of valid commands
                    string[] validCommands = { "look", "status", "take", "quit" };

                    // Check if command is valid
                    if (!validCommands.Contains(command))
                    {
                        Console.WriteLine($"Invalid input: '{input}'. Please try again with a valid command.");
                        continue; 
                    }

                    // Process valid commands
                    switch (command)
                    {
                        case "look":
                            // Display room description
                            Console.WriteLine(currentRoom.GetFullDescription());
                            break;

                        case "status":
                            // Display player status
                            Console.WriteLine(player.GetStatus());
                            break;

                        case "take":
                            // Take item from the room if available
                            if (currentRoom.HasItem())
                            {
                                string item = currentRoom.TakeItem();
                                player.PickUpItem(item);
                                Console.WriteLine($"You picked up the {item}.");
                            }
                            else
                            {
                                Console.WriteLine("There's nothing to take here.");
                            }
                            break;

                        case "quit":
                            // End the game
                            Console.WriteLine("Thanks for playing Dungeon Explorer!");
                            playing = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions during gameplay
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}