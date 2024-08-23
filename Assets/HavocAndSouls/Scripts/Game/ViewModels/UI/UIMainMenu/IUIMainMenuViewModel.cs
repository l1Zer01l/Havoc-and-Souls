/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IUIMainMenuViewModel : IViewModel
    {
        void StartGame(object sender);
        void OpenSettings(object sender);
        void ExitGame(object sender);
    }
}
