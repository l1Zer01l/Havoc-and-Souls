/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Services;
using System.Collections;
using UnityEngine;

namespace HavocAndSouls
{
    public class GamePlayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        private ReactiveProperty<GamePlayExitParams> m_sceneExitParams = new ();
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);
            yield return null;
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_sceneExitParams;
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUIGamePlayViewModel>(factory => new UIGamePlayViewModel(LoadMainMenuParams));
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();

            //Load and Bind UIGamePlay
            var uIGamePlayPrefab = loadService.LoadPrefab<UIGamePlayView>(LoadService.PREFAB_UI_GAME_PLAY);
            var uIGamePlayView = Object.Instantiate(uIGamePlayPrefab);
            var uIGamePlayViewModel = container.Resolve<IUIGamePlayViewModel>();
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();

            uIGamePlayView.Bind(uIGamePlayViewModel);
            uIRootViewModel.AttachSceneUI(uIGamePlayView);

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var uIGamePlayViewModel = m_container.Resolve<IUIGamePlayViewModel>();
                uIGamePlayViewModel.OpenMenuPanel(this);
            }
        }

        private void LoadMainMenuParams(object sender)
        {
            var mainMenuEnterParams = new MainMenuEnterParams("Finaly");
            var exitParams = new GamePlayExitParams(mainMenuEnterParams);
            m_sceneExitParams.SetValue(sender, exitParams);
        }
    }
}
