/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace HavocAndSouls.Infrastructure.MVVM.Binders
{
    public class OnCollisionStayEmptyBinder : CollisionOnTriggerEmptyBinder
    {
        private void OnCollisionStay(Collision collision)
        {
            var view = collision.gameObject.GetComponent(m_trigerViewType);
            if (view)
            {
                m_action?.Invoke(view);
            }
        }
    }
}