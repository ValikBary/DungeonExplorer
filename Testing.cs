using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Testing
    {
        public static bool RunTests()
        {
            Console.WriteLine("Running DungeonExplorer tests...");

            bool allTestsPassed = true;

            // Test Player
            allTestsPassed &= TestPlayer();

            // Test Items
            allTestsPassed &= TestItems();

            // Test Monsters
            allTestsPassed &= TestMonsters();

            Console.WriteLine(allTestsPassed ? "All tests passed!" : "Some tests failed!");
            return allTestsPassed;
        }

        private static bool TestPlayer()
        {
            Console.WriteLine("Testing Player class...");
            try
            {
                Player player = new Player("TestHero", 100);

                // Test initial values
                if (player.Name != "TestHero" || player.Health != 100)
                {
                    Console.WriteLine("FAILED: Player constructor doesn't set properties correctly");
                    return false;
                }

                // Test taking damage
                player.TakeDamage(30);
                if (player.Health != 70)
                {
                    Console.WriteLine("FAILED: Player.TakeDamage() not working correctly");
                    return false;
                }

                // Test healing
                player.Heal(20);
                if (player.Health != 90)
                {
                    Console.WriteLine("FAILED: Player.Heal() not working correctly");
                    return false;
                }

                Console.WriteLine("Player tests passed!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED: Exception in Player tests: {ex.Message}");
                return false;
            }
        }

        private static bool TestItems()
        {
            Console.WriteLine("Testing Item classes...");
            try
            {
                // Test basic Item instead of Weapon
                Item key = new Item("Key", "A rusty old key");
                if (key.Name != "Key")
                {
                    Console.WriteLine("FAILED: Item constructor doesn't set properties correctly");
                    return false;
                }

                // Test Potion
                Potion healthPotion = new Potion("Health Potion", "Restores health", 30);
                if (healthPotion.Name != "Health Potion" || healthPotion.HealingAmount != 30)
                {
                    Console.WriteLine("FAILED: Potion constructor doesn't set properties correctly");
                    return false;
                }

                // Test potion use
                Player player = new Player("Hero", 50);
                string result = healthPotion.Use(player);
                if (player.Health != 80 || !result.Contains("30 health"))
                {
                    Console.WriteLine("FAILED: Potion.Use() not working correctly");
                    return false;
                }

                Console.WriteLine("Item tests passed!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED: Exception in Item tests: {ex.Message}");
                return false;
            }
        }

        private static bool TestMonsters()
        {
            Console.WriteLine("Testing Monster class...");
            try
            {
                // Test Monster creation
                Monster goblin = new Monster("Goblin", Monster.DifficultyLevel.Easy);
                if (goblin.Name != "Goblin" || !goblin.IsAlive())
                {
                    Console.WriteLine("FAILED: Monster constructor doesn't set properties correctly");
                    return false;
                }

                // Test monster attack
                int damage = goblin.Attack();
                if (damage <= 0)
                {
                    Console.WriteLine("FAILED: Monster.Attack() returned non-positive damage");
                    return false;
                }

                // Test monster taking damage
                goblin.TakeDamage(15);
                if (goblin.Health <= 0)
                {
                    Console.WriteLine("FAILED: Easy monster shouldn't die from 15 damage");
                    return false;
                }

                Console.WriteLine("Monster tests passed!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED: Exception in Monster tests: {ex.Message}");
                return false;
            }
        }
    }
}
