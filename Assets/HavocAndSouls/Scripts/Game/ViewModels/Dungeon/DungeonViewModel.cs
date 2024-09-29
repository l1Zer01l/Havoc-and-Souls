/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HavocAndSouls
{
    public class DungeonViewModel : IDungeonViewModel
    {
        public ReactiveCollection<BaseRoomViewModel> RoomSpawned { get; private set; } = new();

        private bool[,] m_roomMap; 
        public void CreateDungeon(int countRoom, int maxDistanceDungeon)
        {
            RoomSpawned.Add(new QuadrupleRoomViewModel(new Vector2Int(maxDistanceDungeon + 1, maxDistanceDungeon + 1)));
            m_roomMap = new bool[maxDistanceDungeon * 2 + 1, maxDistanceDungeon * 2 + 1];
            m_roomMap[maxDistanceDungeon + 1, maxDistanceDungeon + 1] = true;

            for (int i = 0; i < countRoom; i++)
            {
                CreateCurrentRoom(maxDistanceDungeon * 2 + 1);
            }
        }

        public void Dispose()
        {
            
        }

        private void CreateCurrentRoom(int maxDistanceDungeon)
        {
            HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
            foreach (var room in RoomSpawned)
            {
                var positionRoom = room.position;
                if (positionRoom.x > 0 && !m_roomMap[positionRoom.x - 1, positionRoom.y])
                    vacantPlaces.Add(new Vector2Int(positionRoom.x - 1, positionRoom.y));
                if (positionRoom.y > 0 && !m_roomMap[positionRoom.x, positionRoom.y - 1])
                    vacantPlaces.Add(new Vector2Int(positionRoom.x, positionRoom.y - 1));
                if (positionRoom.x < maxDistanceDungeon && !m_roomMap[positionRoom.x + 1, positionRoom.y])
                    vacantPlaces.Add(new Vector2Int(positionRoom.x + 1, positionRoom.y));
                if (positionRoom.y < maxDistanceDungeon && !m_roomMap[positionRoom.x, positionRoom.y + 1])
                    vacantPlaces.Add(new Vector2Int(positionRoom.x, positionRoom.y + 1));
            }

            var newPosition = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            var newRoomViewModel = new QuadrupleRoomViewModel(newPosition);
            m_roomMap[newPosition.x, newPosition.y] = true;
            RoomSpawned.Add(newRoomViewModel);
        }
    }
}
