/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IRoomViewModel : IViewModel
    {
        IDoorViewModel LeftDoor { get; }
        IDoorViewModel RightDoor { get; }
        IDoorViewModel UpDoor { get; }
        IDoorViewModel DownDoor { get; }
        void RotateRoom();
    }
}
