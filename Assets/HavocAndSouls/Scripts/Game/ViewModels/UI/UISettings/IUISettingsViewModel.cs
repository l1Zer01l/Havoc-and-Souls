/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;

namespace HavocAndSouls
{
    public interface IUISettingsViewModel : IViewModel
    {
        void OpenSettings(object sender);      
        void CloseSettings(object sender);
        void ApplySettings(object sender);
        void SetMusicVolume(object sender, float volume);
        void SetSFXVolume(object sender, float volume);
        void SetVoiceVolume(object sender, float volume);
    }
}
