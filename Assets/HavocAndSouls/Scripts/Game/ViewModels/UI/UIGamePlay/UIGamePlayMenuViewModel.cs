/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using System;

namespace HavocAndSouls
{
    public class UIGamePlayMenuViewModel : IUIGamePlayMenuViewModel
    {
        public ReactiveProperty<bool> IsOpenMenu { get; private set; } = new ();
        public IUISettingsViewModel MenuSettingsViewModel { get; private set; }

        private Action m_closeMenuPanelCallBack;
        private Action<object> m_loadMainMenuCallBack;

        public UIGamePlayMenuViewModel(Action<object> loadMainMenuCallBack, Action closeMenuPanelCallBack)
        {
            m_loadMainMenuCallBack = loadMainMenuCallBack;
            m_closeMenuPanelCallBack = closeMenuPanelCallBack;

            MenuSettingsViewModel = new UISettingsViewModel(CloseSettings);
            IsOpenMenu.SetValue(this, true);
        }

        [ReactiveMethod]
        public void ContinueGame(object sender)
        {
            m_closeMenuPanelCallBack?.Invoke();
        }

        [ReactiveMethod]
        public void OpenSettings(object sender)
        {
            IsOpenMenu.SetValue(sender, false);
            MenuSettingsViewModel.OpenSettings(sender);
        }

        [ReactiveMethod]
        public void ExitGame(object sender)
        {
            m_loadMainMenuCallBack?.Invoke(sender);
        }

        private void CloseSettings()
        {
            IsOpenMenu.SetValue(null, true);
        }
    }
}
