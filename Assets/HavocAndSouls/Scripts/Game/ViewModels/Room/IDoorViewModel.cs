/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IDoorViewModel : IViewModel
    {
        void OpenDoor(object sender);

        void CloseDoor(object sender);
    }
}
