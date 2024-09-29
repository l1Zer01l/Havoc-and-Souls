/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public interface IAudioSettingsStateProxy
    {
        ReactiveProperty<float> MusicVolume { get; }
        ReactiveProperty<float> SFXVolume { get; }
        ReactiveProperty<float> VoiceVolume { get; }

        void Update(AudioSettingsState audioSettingsState);
    }
}
