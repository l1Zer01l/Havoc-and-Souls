/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using UnityEngine;

namespace HavocAndSouls
{
    public interface IPlayerStateProxy
    {
        ReactiveProperty<float> Speed { get; }
        ReactiveProperty<Vector3> Position { get; }

    }
}
