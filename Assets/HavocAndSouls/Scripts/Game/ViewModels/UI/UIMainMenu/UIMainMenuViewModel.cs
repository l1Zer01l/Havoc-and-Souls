/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure;
using HavocAndSouls.Services;

namespace HavocAndSouls
{
    public class UIMainMenuViewModel : IUIMainMenuViewModel
    {
        public ReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new ();

        public IUISettingsViewModel UISettingsViewModel { get; private set; }

        private SceneService m_sceneService;
        private Coroutines m_coroutines;
        public UIMainMenuViewModel(SceneService sceneService, Coroutines coroutines)
        {
            m_coroutines = coroutines;
            m_sceneService = sceneService;
            UISettingsViewModel = new UISettingsViewModel(OnCloseSettings);
            IsOpenMenuPanel.SetValue(this, true);
        }

        [ReactiveMethod]
        public void StartGame(object sender)
        {
            m_coroutines.StartCoroutine(m_sceneService.LoadGame());          
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
