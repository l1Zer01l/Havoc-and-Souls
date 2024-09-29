/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using UnityEngine;

namespace HavocAndSouls
{
    public class PlayerPrefsPlayerStateProvider : IPlayerStateProvider
    {
        private const string GAME_PLAY_STATE_KEY = nameof(GAME_PLAY_STATE_KEY);
        public IPlayerStateProxy PlayerState { get; private set; }

        private PlayerState m_playerStateOrigin;

        public IObservable<IPlayerStateProxy> LoadPlayerState()
        {
            if (!PlayerPrefs.HasKey(GAME_PLAY_STATE_KEY))
            {
                ResetPlayerState().Subscribe(newPlayerState => PlayerState = newPlayerState);
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_PLAY_STATE_KEY);
                m_playerStateOrigin = JsonUtility.FromJson<PlayerState>(json);
                PlayerState = new PlayerStateProxy(m_playerStateOrigin);
            }

            return Observable.Return(PlayerState);
        }

        public IObservable<IPlayerStateProxy> ResetPlayerState()
        {
            PlayerState = CreatePlayerStateSettings();
            return Observable.Return(PlayerState);
        }

        public IObservable<bool> SavePlayerState()
        {
            var json = JsonUtility.ToJson(m_playerStateOrigin);
            PlayerPrefs.SetString(GAME_PLAY_STATE_KEY, json);
            return Observable.Return(true);
        }

        private PlayerStateProxy CreatePlayerStateSettings()
        {
            m_playerStateOrigin = new PlayerState()
            {
                speed = 10,
                position = new Vector3(0, 1, 0)

            };

            return new PlayerStateProxy(m_playerStateOrigin);
        }
    }
}
