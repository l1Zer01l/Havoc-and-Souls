/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using UnityEngine;

namespace HavocAndSouls.Infrastructure.MVVM.Binders
{
    public abstract class CollisionOnTriggerEmptyBinder : OnTriggerEmptyBinder
    {
        protected override void OnAwake()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                UnityExtention.AddComponentInEditor<BoxCollider>(transform);
                transform.GetComponent<BoxCollider>().isTrigger = false;
            }
#endif
        }
    }
}