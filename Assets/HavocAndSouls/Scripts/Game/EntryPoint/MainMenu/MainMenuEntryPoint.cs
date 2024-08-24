/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using HavocAndSouls.Services;
using System.Collections;
using UnityEngine;

namespace HavocAndSouls
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;
        private ReactiveProperty<MainMenuExitParams> m_sceneExitParams = new ();
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var mainMenuEnterParams = sceneEnterParams.As<MainMenuEnterParams>();
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);

            yield return null;

            Debug.Log($"Start Main Menu Scene with Result: {mainMenuEnterParams.Result}");
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
            container.RegisterSingleton<IUIMainMenuViewModel>(factory => new UIMainMenuViewModel(LoadGamePlayParams));
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();

            //Load and Bind UIMainMenuView
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            var uIMainMenuPrefab = loadService.LoadPrefab<UIMainMenuView>(LoadService.PREFAB_UI_MAIN_MENU);
            var uIMainMenuView = Object.Instantiate(uIMainMenuPrefab);
            var uIMainMenuViewModel =  container.Resolve<IUIMainMenuViewModel>();

            uIMainMenuView.Bind(uIMainMenuViewModel);
            uIRootViewModel.AttachSceneUI(uIMainMenuView);
        }

        private void LoadGamePlayParams(object sender)
        {
            var gamePlayEnterParams = new GamePlayEnterParams();
            var exitParams = new MainMenuExitParams(gamePlayEnterParams);
            m_sceneExitParams.SetValue(sender, exitParams);
        }
    }
}
