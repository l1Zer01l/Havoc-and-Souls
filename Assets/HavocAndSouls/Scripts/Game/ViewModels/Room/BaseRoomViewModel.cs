/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using UnityEngine;
using HavocAndSouls.Infrastructure.Reactive;


namespace HavocAndSouls
{
    public abstract class BaseRoomViewModel : IRoomViewModel
    {
        public IDoorViewModel LeftDoor { get; protected set; }

        public IDoorViewModel RightDoor { get; protected set; }

        public IDoorViewModel UpDoor { get; protected set; }

        public IDoorViewModel DownDoor { get; protected set; }

        public ReactiveProperty<Vector3Int> AngleRotationRoom { get; private set; } = new();

        public readonly Type typeRoom;
        public readonly Vector2Int position;

        public BaseRoomViewModel(Type typeRoom, Vector2Int position)
        {
            this.typeRoom = typeRoom;
            this.position = position;
        }

        public abstract void Dispose();

        public void RotateRoom()
        {
            int countRotation = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < countRotation; i++)
            {
                AngleRotationRoom.SetValue(null, AngleRotationRoom.Value + new Vector3Int(0, 90, 0));

                var temp = LeftDoor;
                LeftDoor = UpDoor;
                UpDoor = RightDoor;
                RightDoor = DownDoor;
                DownDoor = temp;
            }
        }
    }
}
