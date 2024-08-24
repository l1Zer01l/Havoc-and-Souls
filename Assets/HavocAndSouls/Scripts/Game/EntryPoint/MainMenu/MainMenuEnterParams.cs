/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

namespace HavocAndSouls
{
    public class MainMenuEnterParams : SceneEnterParams
    {
        public string Result { get; }
        public MainMenuEnterParams(string result)
        {
            Result = result;
        }
    }
}
