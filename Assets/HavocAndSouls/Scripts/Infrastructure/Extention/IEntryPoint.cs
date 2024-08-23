/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using System.Collections;

namespace HavocAndSouls
{
    public interface IEntryPoint
    {
        IEnumerator Intialization(DIContainer parentContainer);
    }
}
