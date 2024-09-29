/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using UnityEngine;

namespace HavocAndSouls
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_AUDIO_SETTINGS_STATE_KEY = nameof(GAME_AUDIO_SETTINGS_STATE_KEY);
        public IAudioSettingsStateProxy AudioSettingsState { get; private set; }

        private AudioSettingsState m_audioSettingsStateOrigin;

        public IObservable<IAudioSettingsStateProxy> LoadAudioSettingsState()
        {
            if (!PlayerPrefs.HasKey(GAME_AUDIO_SETTINGS_STATE_KEY))
            {
                ResetAudioSettingsState().Subscribe(newAudioSettingsState => AudioSettingsState = newAudioSettingsState);
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_AUDIO_SETTINGS_STATE_KEY);
                m_audioSettingsStateOrigin = JsonUtility.FromJson<AudioSettingsState>(json);
                if (AudioSettingsState == null)
                    AudioSettingsState = new AudioSettingsStateProxy(m_audioSettingsStateOrigin);
                else
                    AudioSettingsState.Update(m_audioSettingsStateOrigin);
            }

            return Observable.Return(AudioSettingsState);
        }

        public IObservable<IAudioSettingsStateProxy> ResetAudioSettingsState()
        {
            AudioSettingsState = CreateAudioSettingsStateFromSettings();

            SaveAudioSettingsState();
            return Observable.Return(AudioSettingsState);
        }

        public IObservable<bool> SaveAudioSettingsState()
        {
            var json = JsonUtility.ToJson(m_audioSettingsStateOrigin, true);
            PlayerPrefs.SetString(GAME_AUDIO_SETTINGS_STATE_KEY, json);

            return Observable.Return(true);
        }

        public IObservable<bool> ForcedSaveAudioSettingsState()
        {
            SaveAudioSettingsState();
            PlayerPrefs.Save();
            return Observable.Return(true);
        }

        private AudioSettingsStateProxy CreateAudioSettingsStateFromSettings()
        {
            m_audioSettingsStateOrigin = new AudioSettingsState()
            {
                MusicVolume = 8,
                SFXVolume = 8,
                VoiceVolume = 8
            };

            return new AudioSettingsStateProxy(m_audioSettingsStateOrigin);
        }

        public void Dispose()
        {
            
        }
    }
}
