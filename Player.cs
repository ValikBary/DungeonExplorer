using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Player : Creature
    {
      
        private List<Item> inventory = new List<Item>();

        // Constructor that calls the base class constructor
        public Player(string name, int health) : base(name, health, 10) // Default attack power of 10
        {

        }

        // Adds an item to the player's inventory
        public void PickUpItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cannot pick up a null item.");
            }

            inventory.Add(item);
        }

        public List<Item> FindItemsByName(string searchTerm)
        {
            return inventory.Where(item => item.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public void IncreaseAttackPower(int amount)
        {
            AttackPower += amount;
        }

        // Legacy method to maintain compatibility

        public void PickUpItem(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                throw new ArgumentException("Cannot pick up an empty item.");
            }
            // Create a simple item as a placeholder
            Item placeholderItem = new Item(itemName, "A found item");
            inventory.Add(placeholderItem);
        }

        // Checks if the player has a specific item in their inventory
        public bool HasItem(string itemName)
        {
            return inventory.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // Get an item from inventory by name
        public Item GetItem(string itemName)
        {
            return inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // Returns a formatted string containing player's current status
        public string GetStatus()
        {
            string status = $"Name: {Name}\nHealth: {Health}\nAttack Power: {AttackPower}\nInventory: ";
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
            return string.Join(", ", inventory.Select(i => i.Name));
        }
    }
}