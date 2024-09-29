/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/



namespace HavocAndSouls
{
    public static class GamePlayViewModelRegistration
    {

        public static void Register(DIContainer container)
        {
            container.Register<IDungeonViewModel>(factory => new DungeonViewModel());
        }     
    }
}
