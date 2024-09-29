/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.Reactive;
using System.Collections;
using UnityEngine;

namespace HavocAndSouls
{
    public class GamePlayEntryPoint : MonoBehaviour, IEntryPoint
    {
        
        [SerializeField] private DungeonView m_dungeonView;

        private DIContainer m_container;
        private SingleReactiveProperty<GamePlayExitParams> m_sceneExitParams = new ();
        public IEnumerator Intialization(DIContainer parentContainer, SceneEnterParams sceneEnterParams)
        {
            var gamePlayEnterParams = sceneEnterParams.As<GamePlayEnterParams>();

            m_container = parentContainer;
            m_container.RegisterSingleton<IUIGamePlayViewModel>(factory => new UIGamePlayViewModel(factory.Resolve<IGameStateProvider>(), 
                                                                                                   LoadMainMenuParams));

            GamePlayServiceRegistration.Register(m_container, gamePlayEnterParams);
            GamePlayViewModelRegistration.Register(m_container);
            GamePlayViewRegistration.BindView(m_container);

            var dungeonViewModel = m_container.Resolve<IDungeonViewModel>();
            m_dungeonView.Bind(dungeonViewModel);

            dungeonViewModel.CreateDungeon(10, 5);
            yield return null;
        }

        public IObservable<SceneExitParams> Run()
        {
            return m_sceneExitParams;
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

        private void OnDestroy()
        {
            m_container.Dispose();
        }
    }
}
