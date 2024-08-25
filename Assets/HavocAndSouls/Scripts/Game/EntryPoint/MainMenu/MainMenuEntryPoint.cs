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
        private SingleReactiveProperty<MainMenuExitParams> m_sceneExitParams = new ();
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var mainMenuEnterParams = sceneEnterParams.As<MainMenuEnterParams>();

            m_container = parentContainer;
            m_container.RegisterSingleton<IUIMainMenuViewModel>(factory => new UIMainMenuViewModel(LoadGamePlayParams));

            MainMenuRegistration.Register(m_container, mainMenuEnterParams);
            MainMenuViewModelRegistration.Register(m_container);
            MainMenuViewModelRegistration.BindView(m_container);

            yield return null;

            Debug.Log($"Start Main Menu Scene with Result: {mainMenuEnterParams.Result}");
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_sceneExitParams;
        }

        private void LoadGamePlayParams(object sender)
        {
            var gamePlayEnterParams = new GamePlayEnterParams();
            var exitParams = new MainMenuExitParams(gamePlayEnterParams);
            m_sceneExitParams.SetValue(sender, exitParams);
        }
    }
}
