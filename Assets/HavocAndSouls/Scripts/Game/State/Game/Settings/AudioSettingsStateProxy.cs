/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public class AudioSettingsStateProxy : IAudioSettingsStateProxy
    {
        public ReactiveProperty<float> MusicVolume { get; private set; }
        public ReactiveProperty<float> SFXVolume { get; private set; }
        public ReactiveProperty<float> VoiceVolume { get; private set; }

        public AudioSettingsStateProxy(AudioSettingsState audioSettingsState)
        {
            
            MusicVolume = new ReactiveProperty<float>(audioSettingsState.MusicVolume);
            SFXVolume = new ReactiveProperty<float>(audioSettingsState.SFXVolume);
            VoiceVolume = new ReactiveProperty<float>(audioSettingsState.VoiceVolume);
            
            SubscriveValues(audioSettingsState);
        }

        public void Update(AudioSettingsState audioSettingsState)
        {
            MusicVolume.SetValue(null, audioSettingsState.MusicVolume);
            SFXVolume.SetValue(null, audioSettingsState.SFXVolume);
            VoiceVolume.SetValue(null, audioSettingsState.VoiceVolume);
            
            SubscriveValues(audioSettingsState);
        }

        private void SubscriveValues(AudioSettingsState audioSettingsState)
        {
            MusicVolume.Subscribe(newMusicVolume => audioSettingsState.MusicVolume = newMusicVolume);
            SFXVolume.Subscribe(newSFXVolume => audioSettingsState.SFXVolume = newSFXVolume);
            VoiceVolume.Subscribe(newVoiceVolume => audioSettingsState.VoiceVolume = newVoiceVolume);
        }
    }
}
