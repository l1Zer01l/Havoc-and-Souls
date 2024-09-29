/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public interface IPlayerStateProvider
    {
        IPlayerStateProxy PlayerState { get; }

        IObservable<IPlayerStateProxy> LoadPlayerState();
        IObservable<bool> SavePlayerState();
        IObservable<IPlayerStateProxy> ResetPlayerState();
    }
}
