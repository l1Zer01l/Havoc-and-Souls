/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/


using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public interface IGameStateProvider
    {
        public IAudioSettingsStateProxy AudioSettingsState { get; }

        public IObservable<IAudioSettingsStateProxy> LoadAudioSettingsState();

        public IObservable<bool> SaveAudioSettingsState();
        public IObservable<bool> ForcedSaveAudioSettingsState();
        public IObservable<IAudioSettingsStateProxy> ResetAudioSettingsState();
    }
}
