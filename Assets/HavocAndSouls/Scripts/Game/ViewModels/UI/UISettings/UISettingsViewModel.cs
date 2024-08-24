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
        private IBinding m_onCloseSettingsBinding;

        public UISettingsViewModel(Action closeSettingsCallBack = null)
        {
            m_closeSettingsCallBack = closeSettingsCallBack;
            m_onCloseSettingsBinding = IsOpenMenuSettings.Subscribe(ActionToObserver.Map<bool>(OnCloseSettingsCallBack));
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
            m_onCloseSettingsBinding.Dispose();
        }

        private void OnCloseSettingsCallBack(bool value)
        {
            if (!value)
                m_closeSettingsCallBack?.Invoke();   
        }

        
    }
}
