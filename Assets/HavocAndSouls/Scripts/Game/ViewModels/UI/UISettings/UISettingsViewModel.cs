/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure.MVVM;
using System;

namespace HavocAndSouls
{
    public class UISettingsViewModel : IUISettingsViewModel, IDisposable
    {
        public ReactiveProperty<bool> IsOpenMenuSettings { get; private set; } = new();

        private Action m_closeSettingsCallBack;
        private IBinding OnCloseSettingsBinding;

        public UISettingsViewModel(Action CloseSettingsCallBack = null)
        {
            m_closeSettingsCallBack = CloseSettingsCallBack;
            OnCloseSettingsBinding = IsOpenMenuSettings.Subscribe(ActionToObserver.Map<bool>(OnCloseSettingsCallBack));
        }

        [ReactiveMethod]
        public void ApplySettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, false);
            
        }

        [ReactiveMethod]
        public void CloseSettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, false);
            
        }
        
        public void OpenSettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, true);
        }

        public void Dispose()
        {
            OnCloseSettingsBinding.Dispose();
        }

        private void OnCloseSettingsCallBack(bool value)
        {
            if (!value)
                m_closeSettingsCallBack?.Invoke();   
        }

        
    }
}
