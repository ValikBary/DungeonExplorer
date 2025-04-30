using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster : Creature
        {
            public enum DifficultyLevel
            {
                Easy,
                Medium,
                Hard
            }

            public DifficultyLevel Difficulty { get; private set; }
            public int ExperienceReward { get; private set; }

            // Constructor
            public Monster(string name, DifficultyLevel difficulty) : base(name, 0, 0)
            {
                Difficulty = difficulty;

                // Set health and attack power based on difficulty
                switch (difficulty)
                {
                    case DifficultyLevel.Easy:
                        Health = 30;
                        AttackPower = 5;
                        ExperienceReward = 10;
                        break;
                    case DifficultyLevel.Medium:
                        Health = 50;
                        AttackPower = 10;
                        ExperienceReward = 25;
                        break;
                    case DifficultyLevel.Hard:
                        Health = 100;
                        AttackPower = 20;
                        ExperienceReward = 50;
                        break;
                    default:
                        throw new ArgumentException("Invalid difficulty level");
                }
            }

            // Override the Attack method for monsters
            public override int Attack()
            {
                // Basic implementation 
                Random random = new Random();
                int variation = random.Next(-2, 3); // -2 to +2 variation
                return Math.Max(1, AttackPower + variation); // Ensure at least 1 damage
            }

            // Get a description of the monster
            public string GetDescription()
            {
                return $"There is a {Difficulty.ToString().ToLower()} {Name} with {Health} health points!";
            }
        }
    }

