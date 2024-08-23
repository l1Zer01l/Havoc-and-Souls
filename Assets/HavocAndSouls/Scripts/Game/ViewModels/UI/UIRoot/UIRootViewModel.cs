/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public class UIRootViewModel : IUIRootViewModel
    {
        public ReactiveProperty<bool> IsActiveLoadingScreen { get; private set; } = new();

        public UIRootViewModel()
        {

        }

        public void ShowLoadingScreen()
        {
            IsActiveLoadingScreen.SetValue(null, true);
        }
        public void HideLoadingScreen()
        {
            IsActiveLoadingScreen.SetValue(null, false);
        }
    }
}
