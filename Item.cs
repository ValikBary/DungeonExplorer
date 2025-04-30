using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual string Use(Player player)
        {
            if (Name.ToLower() == "key")
            {
                return $"- It might be useful to unlock something.";
            }
            else if (Name.ToLower() == "power stone")
            {
                player.IncreaseAttackPower(5);
                return $"You hold the {Name} tightly and feel its energy course through you. Your attack power increases by 5!";
            }
            return $"You picked up the {Name}.";
        }
    }
}
