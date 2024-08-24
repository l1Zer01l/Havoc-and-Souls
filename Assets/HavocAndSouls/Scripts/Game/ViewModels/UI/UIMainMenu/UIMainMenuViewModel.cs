/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using System;

namespace HavocAndSouls
{
    public class UIMainMenuViewModel : IUIMainMenuViewModel
    {
        public ReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new ();

        public IUISettingsViewModel UISettingsViewModel { get; private set; }

        private Action<object> m_startGameCallBack;
        public UIMainMenuViewModel(Action<object> startGameCallBack)
        {
            m_startGameCallBack = startGameCallBack;
            UISettingsViewModel = new UISettingsViewModel(OnCloseSettings);
            IsOpenMenuPanel.SetValue(this, true);
        }

        [ReactiveMethod]
        public void StartGame(object sender)
        {
            m_startGameCallBack?.Invoke(sender);
        }

        [ReactiveMethod]
        public void OpenSettings(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, false);
            UISettingsViewModel.OpenSettings(sender);
        }

        [ReactiveMethod]
        public void ExitGame(object sender)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif  
        }

        private void OnCloseSettings()
        {
            IsOpenMenuPanel.SetValue(null, true);
        }
    }
}
