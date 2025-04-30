using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        // Add an item to inventory
        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cannot add a null item to inventory");
            }
            items.Add(item);
        }

        // Remove an item from inventory
        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }

        // Remove an item by name
        public bool RemoveItem(string itemName)
        {
            Item itemToRemove = items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                return items.Remove(itemToRemove);
            }
            return false;
        }

        // Check if inventory has an item by name
        public bool HasItem(string itemName)
        {
            return items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // Get an item by name
        public Item GetItem(string itemName)
        {
            return items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        // Get a list of all items
        public List<Item> GetAllItems()
        {
            return new List<Item>(items); // Return a copy to prevent direct modification
        }

        // Count of items in inventory
        public int Count => items.Count;

        // Get a formatted string listing all items
        public string GetItemList()
        {
            if (items.Count == 0)
            {
                return "Inventory is empty";
            }

            return string.Join(", ", items.Select(i => i.Name));
        }
    }
}
