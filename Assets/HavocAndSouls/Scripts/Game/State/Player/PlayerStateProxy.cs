/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using UnityEngine;

namespace HavocAndSouls
{
    public class PlayerStateProxy : IPlayerStateProxy
    {
        public ReactiveProperty<float> Speed { get; private set; }
        public ReactiveProperty<Vector3> Position { get; private set; }

        public PlayerStateProxy(PlayerState playerState)
        {
            Speed = new ReactiveProperty<float>(playerState.speed);
            Position = new ReactiveProperty<Vector3>(playerState.position);

            Speed.Subscribe(newValue => playerState.speed = newValue);
            Position.Subscribe(newValue => playerState.position = newValue);
        }
    }
}
