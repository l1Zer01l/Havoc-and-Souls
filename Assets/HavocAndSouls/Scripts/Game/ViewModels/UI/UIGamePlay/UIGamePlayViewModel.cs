/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure;
using HavocAndSouls.Services;
using UnityEngine;

namespace HavocAndSouls
{
    public class UIGamePlayViewModel : IUIGamePlayViewModel
    {
        public ReactiveProperty<bool> IsOpenMenuPanel { get; private set; } = new ();

        [SubViewModel(typeof(UIGamePlayMenuViewModel))]
        public IUIGamePlayMenuViewModel MenuViewModel { get; private set; }

        public UIGamePlayViewModel(DIContainer container)
        {
            MenuViewModel = new UIGamePlayMenuViewModel(container.Resolve<SceneService>(), container.Resolve<Coroutines>(), CloseMenuPanel);
            CloseMenuPanel();
        }

        public void OpenMenuPanel(object sender)
        {
            IsOpenMenuPanel.SetValue(sender, true);
            Time.timeScale = 0f;
        }

        private void CloseMenuPanel()
        {
            IsOpenMenuPanel.SetValue(this, false);
            Time.timeScale = 1f;
        }

        
    }
}
