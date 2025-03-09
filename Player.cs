using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        
        public string Name { get; private set; }

      
        public int Health { get; private set; }

        // Private collection to store player's inventory items
        private List<string> inventory = new List<string>();

        // Constructor for creating a new Player with a name and initial health
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        // Adds an item to the player's inventory
        public void PickUpItem(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                throw new System.ArgumentException("Cannot pick up an empty item.");
            }

            inventory.Add(item);
        }

        // Checks if the player has a specific item in their inventory
        public bool HasItem(string item)
        {
            return inventory.Contains(item);
        }

        // Reduces the player's health by the specified amount
        public void TakeDamage(int amount)
        {
            if (amount < 0)
            {
                throw new System.ArgumentException("Damage amount cannot be negative.");
            }

            Health = System.Math.Max(0, Health - amount); // Prevent negative health
        }

        // Increases the player's health by the specified amount
        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new System.ArgumentException("Healing amount cannot be negative.");
            }

            Health += amount;
        }

        // Checks if the player is still alive
        public bool IsAlive()
        {
            return Health > 0;
        }

        // Returns a formatted string containing player's current status
        public string GetStatus()
        {
            string status = $"Name: {Name}\nHealth: {Health}\nInventory: ";

            if (inventory.Count > 0)
            {
                status += InventoryContents();
            }
            else
            {
                status += "Empty";
            }

            return status;
        }

        // Returns a comma-separated string of all inventory items
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}