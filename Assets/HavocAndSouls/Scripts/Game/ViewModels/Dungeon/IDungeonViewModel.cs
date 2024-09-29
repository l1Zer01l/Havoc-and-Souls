/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IDungeonViewModel : IViewModel
    {
        void CreateDungeon(int countRoom, int maxDistanceDungeon);
    }
}
