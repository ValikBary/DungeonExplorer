using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Potion : Item
    {
        public int HealingAmount { get; private set; }

        public Potion(string name, string description, int healingAmount)
            : base(name, description)
        {
            HealingAmount = healingAmount;
        }

        public override string Use(Player player)
        {
            player.Heal(HealingAmount);
            return $"You drink the {Name} and restore {HealingAmount} health!";
        }
    }
}
