using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
            // Properties
            public string Name { get; protected set; }
            public int Health { get; protected set; }
            public int AttackPower { get; protected set; }

            // Constructor
            public Creature(string name, int health, int attackPower)
            {
                Name = name;
                Health = health;
                AttackPower = attackPower;
            }

            // Methods
            public virtual int Attack()
            {
                return AttackPower;
            }

            public virtual void TakeDamage(int amount)
            {
                if (amount < 0)
                {
                    throw new ArgumentException("Damage amount cannot be negative.");
                }

                Health = Math.Max(0, Health - amount);
            }

            public bool IsAlive()
            {
                return Health > 0;
            }

            // Method to heal the creature
            public virtual void Heal(int amount)
            {
                if (amount < 0)
                {
                    throw new ArgumentException("Healing amount cannot be negative.");
                }

                Health += amount;
            }
        }
    }


