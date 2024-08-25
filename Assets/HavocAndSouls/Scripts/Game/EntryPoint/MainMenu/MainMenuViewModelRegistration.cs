/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Services;
using UnityEngine;

namespace HavocAndSouls
{
    public static class MainMenuViewModelRegistration
    {
        public static void Register(DIContainer container)
        {

        }

        public static void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();

            //Load and Bind UIMainMenuView
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            var uIMainMenuPrefab = loadService.LoadPrefab<UIMainMenuView>(LoadService.PREFAB_UI_MAIN_MENU);
            var uIMainMenuView = Object.Instantiate(uIMainMenuPrefab);
            var uIMainMenuViewModel = container.Resolve<IUIMainMenuViewModel>();

            uIMainMenuView.Bind(uIMainMenuViewModel);
            uIRootViewModel.AttachSceneUI(uIMainMenuView);
        }
    }
}
