/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace HavocAndSouls
{
    public class QuadrupleRoomViewModel : BaseRoomViewModel
    {
        public QuadrupleRoomViewModel(Vector2Int position) : base(typeof(QuadrupleRoomViewModel), position)
        {
            LeftDoor = new DoorViewModel();
            RightDoor = new DoorViewModel();
            UpDoor = new DoorViewModel();
            DownDoor = new DoorViewModel();
        }

        public override void Dispose()
        {
            
        }

    }
}
