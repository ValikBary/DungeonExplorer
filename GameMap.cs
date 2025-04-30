using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonExplorer
    {
        public class GameMap
        {
            private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
            private string currentRoomId;

            // Constructor
            public GameMap()
            {
                
            }

            // Add a room to the map
            public void AddRoom(string roomId, Room room)
            {
                if (string.IsNullOrEmpty(roomId))
                {
                    throw new ArgumentException("Room ID cannot be empty");
                }
                if (room == null)
                {
                    throw new ArgumentNullException(nameof(room));
                }
                if (rooms.ContainsKey(roomId))
                {
                    throw new ArgumentException($"Room with ID {roomId} already exists");
                }

                rooms.Add(roomId, room);

                // If this is the first room, set it as current
                if (rooms.Count == 1)
                {
                    currentRoomId = roomId;
                }
            }

            // Get the current room
            public Room GetCurrentRoom()
            {
                if (string.IsNullOrEmpty(currentRoomId) || !rooms.ContainsKey(currentRoomId))
                {
                    throw new InvalidOperationException("Current room is not set or is invalid");
                }
                return rooms[currentRoomId];
            }

            // Move to a different room
            public bool MoveToRoom(string roomId)
            {
                if (!rooms.ContainsKey(roomId))
                {
                    return false;
                }

                currentRoomId = roomId;
                return true;
            }

            // Returns a list of possible exits from the current room
            public List<string> GetExits()
            {
                Room currentRoom = GetCurrentRoom();
                return currentRoom.GetConnections();
            }
        }
    }
