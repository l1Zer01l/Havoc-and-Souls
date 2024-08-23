/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;
using System;

namespace HavocAndSouls
{
    public interface IUISettingsViewModel : IViewModel
    {
        void OpenSettings(object sender);
        void CloseSettings(object sender);
        void ApplySettings(object sender);
    }
}
