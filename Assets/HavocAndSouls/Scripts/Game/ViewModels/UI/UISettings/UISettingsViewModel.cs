/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System;
using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public class UISettingsViewModel : IUISettingsViewModel
    {
        public ReactiveProperty<bool> IsOpenMenuSettings { get; private set; } = new();

        public ReactiveProperty<float> MusicVolume { get; private set; }
        public ReactiveProperty<float> SFXVolume { get; private set; }
        public ReactiveProperty<float> VoiceVolume { get; private set; }

        private Action m_closeSettingsCallBack;
        private IBinding m_onCloseSettingsBinding;
        private IGameStateProvider m_gameStateProvider;

        public UISettingsViewModel(IGameStateProvider gameStateProvider, Action closeSettingsCallBack = null)
        {
            m_gameStateProvider = gameStateProvider;

            MusicVolume = m_gameStateProvider.AudioSettingsState.MusicVolume;
            SFXVolume = m_gameStateProvider.AudioSettingsState.SFXVolume;
            VoiceVolume = m_gameStateProvider.AudioSettingsState.VoiceVolume;

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
            m_gameStateProvider.LoadAudioSettingsState();          
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
