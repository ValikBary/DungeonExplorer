using System;
using System.Media;
using System.Linq;


namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private GameMap gameMap;


        public Game()
        {

            try
            {
                // Create the game map
                gameMap = new GameMap();

                // Create various rooms
                Room startRoom = new Room("You awaken in a mysterious underground complex, memories foggy... There is a door to the north.");
                Room northRoom = new Room("This is a large chamber with cobwebs in the corners. There is a passage to the south.");

                // Add items to rooms
                Item rustyKey = new Item("key", "A rusty old key");
                startRoom.AddItem(rustyKey);

                Potion healthPotion = new Potion("health potion", "A small blue bottle", 20);
                northRoom.AddItem(healthPotion);

                // Add a monster to the north room
                Monster goblin = new Monster("Goblin", Monster.DifficultyLevel.Easy);
                northRoom.AddMonster(goblin);

                // Connect the rooms
                startRoom.AddConnection("north", "northRoom");
                northRoom.AddConnection("south", "startRoom");

                // Add rooms to the map
                gameMap.AddRoom("startRoom", startRoom);
                gameMap.AddRoom("northRoom", northRoom);

                // Third room to the east
                Room eastRoom = new Room("A small, dimly lit chamber with ancient runes carved into the walls. A narrow passage leads back west.");

                // A magical stone to the east room
                Item powerStone = new Item("power stone", "A glowing blue crystal that pulses with energy");
                eastRoom.AddItem(powerStone);

                // Add a tougher monster to the east room
                Monster skeleton = new Monster("Skeleton", Monster.DifficultyLevel.Medium);
                eastRoom.AddMonster(skeleton);

                // Connect the rooms
                startRoom.AddConnection("east", "eastRoom");
                eastRoom.AddConnection("west", "startRoom");

                // Add the new room to the map
                gameMap.AddRoom("eastRoom", eastRoom);

                // Name validation loop
                string playerName;
                do
                {
                    Console.Write("Enter your character's name: ");
                    playerName = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(playerName))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                    }
                    else if (!playerName.Any(char.IsLetter))
                    {
                        Console.WriteLine("Name must contain at least one letter. Please enter a valid name.");
                    }
                } while (string.IsNullOrWhiteSpace(playerName) || !playerName.Any(char.IsLetter));

                // Create player with initial health and attack power
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

            // Display initial room description
            Console.WriteLine(gameMap.GetCurrentRoom().GetFullDescription());
            while (playing)
            {
                try
                {
                    // Display prompt and get player command
                    Console.Write("\nWhat would you like to do? (look, status, take [item], go [direction], fight, quit): ");
                    string input = Console.ReadLine().Trim();

                    // Convert to lowercase for case-insensitive comparison
                    string[] commandParts = input.ToLower().Split(new char[] { ' ' }, 2);
                    string command = commandParts[0];

                    // List of valid commands
                    string[] validCommands = { "look", "status", "take", "go", "fight", "quit" };

                    // Check if command is valid
                    if (!validCommands.Contains(command))
                    {
                        Console.WriteLine($"Invalid input: '{input}'. Please try again with a valid command.");
                        continue;
                    }

                    
                    switch (command)
                    {
                        case "look":
                            // Display room description
                            Console.WriteLine(gameMap.GetCurrentRoom().GetFullDescription());
                            break;

                        case "status":
                            // Display player status
                            Console.WriteLine(player.GetStatus());

                            // Also display game statistics
                            Console.WriteLine("\n" + Statistics.Instance.GetStatsSummary());
                            break;

                        case "take":
                            // Take item from the room if available
                            if (commandParts.Length < 2)
                            {
                                Console.WriteLine("Take what? Please specify an item.");
                                break;
                            }

                            string itemName = commandParts[1];
                            Room currentRoom = gameMap.GetCurrentRoom();

                            if (currentRoom.HasItem(itemName))
                            {
                                try
                                {
                                    Item item = currentRoom.TakeItem(itemName);
                                    // Add this line to store the item in player's inventory
                                    player.PickUpItem(item);
                                    Console.WriteLine($"You picked up the {item.Name}.");

                                    // Statistics tracking 
                                    Statistics.Instance.ItemCollected();

                                    // Use the item
                                    string useResult = item.Use(player);
                                    Console.WriteLine(useResult);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Couldn't take item: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"There's no {itemName} here to take.");
                            }
                            break;
                            

                        case "go":
                            if (commandParts.Length < 2)
                            {
                                Console.WriteLine("Go where? Please specify a direction.");
                                break;
                            }

                            string direction = commandParts[1];
                            Room room = gameMap.GetCurrentRoom();

                            Statistics.Instance.RoomVisited();

                            if (room.HasExit(direction))
                            {
                                string roomId = room.GetConnection(direction);
                                gameMap.MoveToRoom(roomId);
                                Console.WriteLine($"You go {direction}.");
                                Console.WriteLine(gameMap.GetCurrentRoom().GetFullDescription());
                            }
                            else
                            {
                                Console.WriteLine($"You can't go {direction} from here.");
                            }
                            break;

                        case "fight":
                            Room currentRoom2 = gameMap.GetCurrentRoom();
                            if (currentRoom2.HasMonster())
                            {
                                Monster monster = currentRoom2.GetMonster();

                                // Player attacks first
                                int playerDamage = player.Attack();
                                Console.WriteLine($"You attack the {monster.Name} for {playerDamage} damage!");
                                monster.TakeDamage(playerDamage);

                                if (!monster.IsAlive())
                                {
                                    Console.WriteLine($"You defeated the {monster.Name}!");
                                    Statistics.Instance.MonsterDefeated();
                                    break;
                                }

                                // Monster counter-attacks
                                int monsterDamage = monster.Attack();
                                Console.WriteLine($"The {monster.Name} attacks you for {monsterDamage} damage!");
                                player.TakeDamage(monsterDamage);

                                

                                if (!player.IsAlive())
                                {
                                    Console.WriteLine("You have been defeated! Game over.");
                                    playing = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("There's nothing to fight here.");
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