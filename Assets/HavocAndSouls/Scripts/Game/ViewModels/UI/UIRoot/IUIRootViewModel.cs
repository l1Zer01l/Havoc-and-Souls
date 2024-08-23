/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IUIRootViewModel : IViewModel    
    {
        void ShowLoadingScreen();
        void HideLoadingScreen();
    }
}
