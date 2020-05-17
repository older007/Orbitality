using System;
using UnityEngine;

namespace OrbitalSystem.Element
{
    public class ElementPhysics : MonoBehaviour, IOrbitalElementPhysics
    {
        [SerializeField] private bool destroyAll = false;
        public void SetLayer(int layer)
        {
            gameObject.layer = layer;
        }

        public event Action<IGravity> TriggerEnter 
        {
            add => triggerEnter += value;
            remove => triggerEnter -= value;
        }

        private Action<IGravity> triggerEnter;
        
        public event Action<IDamageElementBase> CollisionEnter 
        {
            add => collisionEnter += value;
            remove => collisionEnter -= value;
        }

        private Action<IDamageElementBase> collisionEnter;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<IGravity>(out var data))
            {
                triggerEnter?.Invoke(data);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageElementBase>(out var data))
            {
                if (destroyAll)
                {
                    data.Destroy();
                }

                collisionEnter?.Invoke(data);
            }
        }
    }
}