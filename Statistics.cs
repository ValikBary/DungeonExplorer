using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Statistics
    {
        
        private static Statistics instance;
        public static Statistics Instance
        {
            get
            {
                if (instance == null)
                    instance = new Statistics();
                return instance;
            }
        }

        // Game statistics
        public int MonstersDefeated { get; private set; }
        public int ItemsCollected { get; private set; }
        public int RoomsVisited { get; private set; }
        public DateTime GameStartTime { get; private set; }

        private Statistics()
        {
            // Initialize statistics
            MonstersDefeated = 0;
            ItemsCollected = 0;
            RoomsVisited = 1; // Start in one room
            GameStartTime = DateTime.Now;
        }

        public void MonsterDefeated()
        {
            MonstersDefeated++;
        }

        public void ItemCollected()
        {
            ItemsCollected++;
        }

        public void RoomVisited()
        {
            RoomsVisited++;
        }

        public TimeSpan GetPlayTime()
        {
            return DateTime.Now - GameStartTime;
        }

        public string GetStatsSummary()
        {
            TimeSpan playTime = GetPlayTime();
            return $"Game Statistics:\n" +
                   $"- Play time: {playTime.Hours}h {playTime.Minutes}m {playTime.Seconds}s\n" +
                   $"- Rooms visited: {RoomsVisited}\n" +
                   $"- Items collected: {ItemsCollected}\n" +
                   $"- Monsters defeated: {MonstersDefeated}";
        }
    }
}
