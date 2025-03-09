namespace DungeonExplorer
{
    public class Room
    {
        
        private string description;

        private string item;

        private bool itemTaken;

        // Constructor to initialize a room with a description
        public Room(string description)
        {
            this.description = description;
            this.item = null;
            this.itemTaken = false;
        }

        // Constructor to initialize a room with a description and an item
        public Room(string description, string item)
        {
            this.description = description;
            this.item = item;
            this.itemTaken = false;
        }

        // Returns the description of the room
        public string GetDescription()
        {
            return description;
        }

        // Adds an item to the room
        public void AddItem(string newItem)
        {
            try
            {
                if (string.IsNullOrEmpty(newItem))
                {
                    throw new System.ArgumentException("Cannot add an empty item to the room.");
                }

                this.item = newItem;
                this.itemTaken = false;
            }
            catch (System.ArgumentException ex)
            {
                // Re-throw the exception to be handled by the calling method
                throw ex;
            }
        }

        // Checks if the room has an item that hasn't been taken
        public bool HasItem()
        {
            return item != null && !itemTaken;
        }

        // Returns the item in the room if it exists and hasn't been taken
        public string GetItem()
        {
            return HasItem() ? item : null;
        }

        // Takes the item from the room if possible
        public string TakeItem()
        {
            try
            {
                if (!HasItem())
                {
                    throw new System.InvalidOperationException("There is no item to take in this room.");
                }

                itemTaken = true;
                return item;
            }
            catch (System.InvalidOperationException ex)
            {
                // Log the error (in a real application)
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Gets the full description of the room, including any items
        public string GetFullDescription()
        {
            if (HasItem())
            {
                return $"{description}\nYou see a {item} here.";
            }
            return description;
        }
    }
}