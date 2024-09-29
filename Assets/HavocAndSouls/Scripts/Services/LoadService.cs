/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace HavocAndSouls.Services
{
    public class LoadService : System.IDisposable
    {
        public const string PREFAB_UI_ROOT = "UI/UIRoot";
        public const string PREFAB_UI_MAIN_MENU = "UI/UIMainMenu";
        public const string PREFAB_UI_GAME_PLAY = "UI/UIGamePlay";

        public LoadService()
        {

        }

        public void Dispose()
        {
            
        }

        public T LoadPrefab<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            if (prefab is null)
            {
                Debug.LogError($"Can't load prefab from: {path}");
                throw new System.MethodAccessException();
            }
            return prefab;
        }
    }
}
