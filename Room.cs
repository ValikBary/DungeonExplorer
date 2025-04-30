using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Room
    {

        private string description;
        private List<Item> items = new List<Item>();
        private Dictionary<string, bool> itemsTaken = new Dictionary<string, bool>();
        private Monster monster;
        private Dictionary<string, string> connections = new Dictionary<string, string>();
    public Room(string description)
        {
            this.description = description;

        }

        // Add a monster to the room
        public void AddMonster(Monster monster)
        {
            this.monster = monster;
        }

        // Check if the room has a monster that's alive
        public bool HasMonster()
        {
            return monster != null && monster.IsAlive();
        }

        // Get the monster in the room
        public Monster GetMonster()
        {
            return monster;
        }

        // Returns the description of the room
        public string GetDescription()
        {
            return description;
        }

        // Adds an item to the room
        public void AddItem(Item newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem), "Cannot add a null item to the room.");
            }

            items.Add(newItem);
            itemsTaken[newItem.Name] = false;
        }

        // Checks if the room has a specific item that hasn't been taken
        public bool HasItem(string itemName)
        {
            return items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase) &&
                         !itemsTaken.ContainsKey(itemName) || !itemsTaken[itemName]);
        }

        // Checks if the room has any items that haven't been taken
        public bool HasItems()
        {
            return items.Any(i => !itemsTaken.ContainsKey(i.Name) || !itemsTaken[i.Name]);
        }

        // Returns all items in the room that haven't been taken
        public List<Item> GetItems()
        {
            return items.Where(i => !itemsTaken.ContainsKey(i.Name) || !itemsTaken[i.Name]).ToList();
        }

        // Takes a specific item from the room if possible
        public Item TakeItem(string itemName)
        {
            Item item = items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                throw new InvalidOperationException($"There is no {itemName} in this room.");
            }

            if (itemsTaken.ContainsKey(item.Name) && itemsTaken[item.Name])
            {
                throw new InvalidOperationException($"The {itemName} has already been taken.");
            }

            itemsTaken[item.Name] = true;
            return item;
        }

        // Add a connection to another room
        public void AddConnection(string direction, string roomId)
        {
            connections[direction.ToLower()] = roomId;
        }

        // Check if the room is connected to another room in the specified direction
        public bool HasExit(string direction)
        {
            return connections.ContainsKey(direction.ToLower());
        }

        // Get the ID of the connected room in the specified direction
        public string GetConnection(string direction)
        {
            if (!HasExit(direction))
            {
                return null;
            }

            return connections[direction.ToLower()];
        }

        // Get a list of all available directions from this room
        public List<string> GetConnections()
        {
            return connections.Keys.ToList();
        }

        // Check if this room is connected to a specific room ID
        public bool IsConnectedTo(string roomId)
        {
            return connections.Values.Contains(roomId);
        }

        // Gets the full description of the room, including monsters and items
        public string GetFullDescription()
        {
            string result = description;

            // Add monster description if there is one
            if (HasMonster())
            {
                result += $"\n{monster.GetDescription()}";
            }

            // Add item descriptions
            var availableItems = GetItems();
            if (availableItems.Count > 0)
            {
                result += "\nYou see the following items:";
                foreach (var item in availableItems)
                {
                    result += $"\n- {item.Name}: {item.Description}";
                }
            }

            // Add available exits
            if (connections.Count > 0)
            {
                result += "\n\nDirections:";
                foreach (var exit in connections)
                {
                    result += $"\n- {exit.Key}";
                }
            }

            return result;
        }
    }
}