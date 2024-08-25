/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public interface IAudioSettingsStateProxy
    {
        public ReactiveProperty<float> MusicVolume { get; }
        public ReactiveProperty<float> SFXVolume { get; }
        public ReactiveProperty<float> VoiceVolume { get; }
    }
}
