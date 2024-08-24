/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IUIGamePlayMenuViewModel : IViewModel
    {           
        void ContinueGame(object sender);       
        void OpenSettings(object sender);
        void ExitGame(object sender);

    }
}
