using System;
using UnityEngine;

namespace Utils
{
    public class UpdateProvider : MonoBehaviour
    {
        public bool IsStoped;
        public event Action UpdateEvent 
        {
            add => updateEvent += value;
            remove => updateEvent -= value;
        }

        private Action updateEvent;
        
        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        public void Update()
        {
            if (IsStoped)
            {
                return;
            }

            updateEvent?.Invoke();
        }

        private void OnDestroy()
        {
            updateEvent = null;
        }
    }
}