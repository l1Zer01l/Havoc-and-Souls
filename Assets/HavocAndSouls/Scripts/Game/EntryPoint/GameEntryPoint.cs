/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Infrastructure;
using UnityEngine.SceneManagement;
using HavocAndSouls.Services;
using System.Collections;
using UnityEngine;

namespace HavocAndSouls
{
    public class GameEntryPoint
    {
        private static GameEntryPoint m_instance;

        private DIContainer m_rootContainer;
        private Coroutines m_coroutines;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Start()
        {
            Application.targetFrameRate = 144;

            m_instance = new GameEntryPoint();
            m_instance.Init();
        }

        private GameEntryPoint()
        {
            m_rootContainer = new DIContainer();

            RegisterService(m_rootContainer);
            RegisterViewModel(m_rootContainer);
            BindView(m_rootContainer);

            //Init Coroutines
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(m_coroutines.gameObject);
            m_rootContainer.RegisterInstance(m_coroutines);
        }

        private void Init()
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            sceneService.LoadSceneEvent += OnLoadScene;

            if (sceneService.GetCurrentSceneName() != SceneService.BOOT_STRAP_SCENE)
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            var mainMenuDefaultEnterParams = new MainMenuEnterParams("Default");
            m_coroutines.StartCoroutine(LoadAndStartMainMenu(mainMenuDefaultEnterParams));
        }

        private void RegisterService(DIContainer container)
        {
            container.RegisterSingleton(factory => new SceneService());

            container.RegisterSingleton(factory => new LoadService());

            //----------- After Load Service ------------------
             
        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUIRootViewModel>(factory => new UIRootViewModel());
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();

            //Bind UIRootView
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            var uIRootViewPrefab = loadService.LoadPrefab<UIRootView>(LoadService.PREFAB_UI_ROOT);
            var uIRootView = Object.Instantiate(uIRootViewPrefab);
            Object.DontDestroyOnLoad(uIRootView.gameObject);
            uIRootView.Bind(uIRootViewModel);

        }

        private void OnLoadScene(Scene scene, LoadSceneMode mode, SceneEnterParams sceneEnterParams)
        {
            var sceneName = scene.name;

            if (sceneName.Equals(SceneService.BOOT_STRAP_SCENE))
                LoadBootStrap();
            else if (sceneName.Equals(SceneService.MAIN_MENU_SCENE))
                m_coroutines.StartCoroutine(LoadMainMenu(sceneEnterParams));
            else if (sceneName.Equals(SceneService.GAMEPLAY_SCENE))
                m_coroutines.StartCoroutine(LoadGamePlay(sceneEnterParams));
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams)
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadMenu(mainMenuEnterParams);
        }

        private IEnumerator LoadAndStartGamePlay(GamePlayEnterParams gamePlayEnterParams)
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadGame(gamePlayEnterParams);
        }

        private void LoadBootStrap()
        {
            Time.timeScale = 0f;
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            uIRootViewModel.ShowLoadingScreen();
        }

        private IEnumerator LoadMainMenu(SceneEnterParams sceneEnterParams)
        {
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();

            var mainMenuContainer = new DIContainer(m_rootContainer);

            var mainMenuEntryPoint = UnityExtention.GetEntryPoint<MainMenuEntryPoint>();
            yield return mainMenuEntryPoint.Intialization(mainMenuContainer, sceneEnterParams);

            mainMenuEntryPoint.Run().Subscribe(ActionToObserver.Map<SceneExitParams>(sceneExitParams =>
            {
                m_coroutines.StartCoroutine(LoadAndStartGamePlay(sceneExitParams.TargetEnterParams.As<GamePlayEnterParams>()));
            }));

            uIRootViewModel.HideLoadingScreen();

            Time.timeScale = 1f;
        }

        private IEnumerator LoadGamePlay(SceneEnterParams sceneEnterParams)
        {
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();

            var gamePlayContainer = new DIContainer(m_rootContainer);

            var gamePlayEntryPoint = UnityExtention.GetEntryPoint<GamePlayEntryPoint>();
            yield return gamePlayEntryPoint.Intialization(gamePlayContainer, sceneEnterParams);

            gamePlayEntryPoint.Run().Subscribe(ActionToObserver.Map<SceneExitParams>(sceneExitParams =>
            {
                m_coroutines.StartCoroutine(LoadAndStartMainMenu(sceneExitParams.TargetEnterParams.As<MainMenuEnterParams>()));
            }));

            uIRootViewModel.HideLoadingScreen();
            
            Time.timeScale = 1f;
        }

    }
}
