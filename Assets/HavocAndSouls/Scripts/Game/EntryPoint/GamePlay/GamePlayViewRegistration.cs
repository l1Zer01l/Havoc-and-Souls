/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Services;
using UnityEngine;

namespace HavocAndSouls
{
    public static class GamePlayViewRegistration
    {
        public static void BindView(DIContainer container)
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
    }
}
