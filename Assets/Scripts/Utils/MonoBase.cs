using System;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class MonoBase : MonoBehaviour
    {
        private UpdateProvider updateProvider => ServiceLocator.Get<UpdateProvider>();
        
        private void Start()
        {
            updateProvider.UpdateEvent += OnUpdate;
        }

        protected virtual void OnDestroy()
        {
            if (!updateProvider)
            {
                return;
            }

            updateProvider.UpdateEvent -= OnUpdate;
        }

        protected virtual void OnUpdate()
        {
            
        }
    }
}