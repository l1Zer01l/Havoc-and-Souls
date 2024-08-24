/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure;
using HavocAndSouls.Services;
using System;

namespace HavocAndSouls
{
    public class UIGamePlayMenuViewModel : IUIGamePlayMenuViewModel
    {
        public ReactiveProperty<bool> IsOpenMenu { get; private set; } = new ();
        public IUISettingsViewModel MenuSettingsViewModel { get; private set; }

        private SceneService m_sceneService;
        private Coroutines m_coroutines;
        private Action m_closeMenuPanelCallBack;

        public UIGamePlayMenuViewModel(SceneService sceneSerivce, Coroutines coroutines, Action closeMenuPanelCallBack)
        {
            m_sceneService = sceneSerivce;
            m_coroutines = coroutines;
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
            m_coroutines.StartCoroutine(m_sceneService.LoadMenu());
        }

        private void CloseSettings()
        {
            IsOpenMenu.SetValue(null, true);
        }
    }
}
