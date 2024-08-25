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

        public ReactiveProperty<float> MusicVolume { get; private set; } = new();
        public ReactiveProperty<float> SFXVolume { get; private set; } = new();
        public ReactiveProperty<float> VoiceVolume { get; private set; } = new();

        private Action m_closeSettingsCallBack;
        private IBinding m_onCloseSettingsBinding;
        private IGameStateProvider m_gameStateProvider;
        public UISettingsViewModel(IGameStateProvider gameStateProvider, Action closeSettingsCallBack = null)
        {
            m_gameStateProvider = gameStateProvider;
            MusicVolume.SetValue(m_gameStateProvider, m_gameStateProvider.AudioSettingsState.MusicVolume.Value);
            SFXVolume.SetValue(m_gameStateProvider, m_gameStateProvider.AudioSettingsState.SFXVolume.Value);
            VoiceVolume.SetValue(m_gameStateProvider, m_gameStateProvider.AudioSettingsState.VoiceVolume.Value);

            MusicVolume.Subscribe(newValue => m_gameStateProvider.AudioSettingsState.MusicVolume.SetValue(null, newValue));
            SFXVolume.Subscribe(newValue => m_gameStateProvider.AudioSettingsState.SFXVolume.SetValue(null, newValue));
            VoiceVolume.Subscribe(newValue => m_gameStateProvider.AudioSettingsState.VoiceVolume.SetValue(null, newValue));

            m_closeSettingsCallBack = closeSettingsCallBack;
            m_onCloseSettingsBinding = IsOpenMenuSettings.Subscribe(OnCloseSettingsCallBack);
        }

        [ReactiveMethod]
        public void ApplySettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, false);
            m_gameStateProvider.SaveAudioSettingsState();
        }

        [ReactiveMethod]
        public void CloseSettings(object sender)
        {
            IsOpenMenuSettings.SetValue(sender, false);       
        }

        [ReactiveMethod]
        public void SetMusicVolume(object sender, float volume)
        {
            MusicVolume.SetValue(sender, volume);
        }

        [ReactiveMethod]
        public void SetSFXVolume(object sender, float volume)
        {
            SFXVolume.SetValue(sender, volume);
        }

        [ReactiveMethod]
        public void SetVoiceVolume(object sender, float volume)
        {
            VoiceVolume.SetValue(sender, volume);
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
