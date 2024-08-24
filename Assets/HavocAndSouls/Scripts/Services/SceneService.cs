/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;
using System;

namespace HavocAndSouls.Services
{
    public class SceneService : IDisposable
    {
        public const string BOOT_STRAP_SCENE = "BootStrap";
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string GAMEPLAY_SCENE = "GamePlay";

        public event UnityAction<Scene, LoadSceneMode, SceneEnterParams> LoadSceneEvent;

        private SceneEnterParams m_targerEnterParams;

        public SceneService() 
        {
            SceneManager.sceneLoaded += OnLoadScene;
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnLoadScene;
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public IEnumerator LoadMenu(MainMenuEnterParams sceneEnterParams)
        {
            m_targerEnterParams = sceneEnterParams;
            yield return LoadScene(BOOT_STRAP_SCENE);
            yield return LoadScene(MAIN_MENU_SCENE);
        }

        public IEnumerator LoadGame(GamePlayEnterParams sceneEnterParams)
        {
            m_targerEnterParams = sceneEnterParams;
            yield return LoadScene(BOOT_STRAP_SCENE);
            yield return LoadScene(GAMEPLAY_SCENE);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            LoadSceneEvent?.Invoke(scene, loadSceneMode, m_targerEnterParams);
        }      
    }
}
