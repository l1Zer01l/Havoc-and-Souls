/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Services;
using System.Collections;
using UnityEngine;

namespace HavocAndSouls
{
    public class GamePlayEntryPoint : MonoBehaviour, IEntryPoint
    {
        private DIContainer m_container;

        public IEnumerator Intialization(DIContainer parentContainer)
        {
            m_container = parentContainer;

            RegisterService(m_container);
            RegisterViewModel(m_container);
            BindView(m_container);
            yield return null;
        }

        private void RegisterService(DIContainer container)
        {

        }

        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUIGamePlayViewModel>(factory => new UIGamePlayViewModel(factory));
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
    }
}
