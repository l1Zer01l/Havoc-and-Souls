/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;
using UnityEngine;

namespace HavocAndSouls
{
    public class UIRootView : View
    {
        [SerializeField] private Transform m_sceneUIContainer;
        public void AddSceneUI(View sceneView)
        {
            sceneView.transform.SetParent(m_sceneUIContainer, false);
        }

        public void ClearSceneUIContainer()
        {
            var childCount = m_sceneUIContainer.childCount;

            for (var i = 0; i < childCount; i++)
            {
                Destroy(m_sceneUIContainer.GetChild(i).gameObject);
            }
        }
    }


}
