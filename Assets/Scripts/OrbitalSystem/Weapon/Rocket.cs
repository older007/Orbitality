using BattleSystem;
using UnityEngine;

namespace OrbitalSystem.Weapon
{
    public class Rocket : MonoBehaviour, IDamageVectorTarget<RocketData>, IGravity
    {
        [SerializeField] private Rigidbody rigidbody;
        
        private RocketData rocketData;
        private Vector3 targetPosition;
        
        public float Damage => rocketData.Damage;
        public bool Destroyed => destroyed;

        private bool destroyed = false;
        private bool isInited;
        
        public void Init(RocketData data, Vector3 targetPos)
        {
            rocketData = data;
            targetPosition = targetPos;
            isInited = true;

            var weight = data.Weight;
            
            rigidbody.mass = weight;
            transform.localScale = new Vector3(weight,weight,weight);
        }

        public void Destroy()
        {
            destroyed = true;
            
            Destroy(gameObject);
        }

        public float Move()
        {
            if (!isInited)
            {
                return 0;
            }

            var tr = transform;
            
            tr.forward = targetPosition;
            rigidbody.velocity = tr.forward * rocketData.Speed;
            
            var distance = Vector3.Distance(tr.position, Vector3.zero);

            return distance;
        }

        public void AddGravitation(Vector3 position)
        {
            targetPosition -= position * 0.05f;
        }
    }
}