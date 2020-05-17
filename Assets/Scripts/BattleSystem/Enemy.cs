using System;
using OrbitalSystem;
using OrbitalSystem.Element;
using UnityEngine;

namespace BattleSystem
{
    public class Enemy : MonoBehaviour, IDamageTransformTarget<EnemyWave>
    {
        //[SerializeField] private Transform moveTransform;
        [SerializeField] private ElementPhysics elementPhysics;
        
        private Action<Enemy> onDestroy;
        private EnemyWave enemyWaveData;
        private Transform targetTransform;
        private bool isDestroyed;

        public event Action<Enemy> OnDestroyed 
        {
            add => onDestroy += value;
            remove => onDestroy -= value;
        }
        
        public void Init(EnemyWave data, Transform targetPos)
        {
            enemyWaveData = data;
            targetTransform = targetPos;
            
            elementPhysics.CollisionEnter += ElementPhysicsOnCollisionEnter;
        }

        private void ElementPhysicsOnCollisionEnter(IDamageElementBase obj)
        {
            enemyWaveData.Hp -= obj.Damage;

            if (enemyWaveData.Hp <= 0)
            {
                onDestroy.Invoke(this);
            }
        }

        public float Damage => enemyWaveData.Damage;
        public bool Destroyed => isDestroyed;
        

        public void Destroy()
        {
            isDestroyed = true;
            onDestroy.Invoke(this);
        }

        public float Move()
        {
            if (targetTransform == null)
            {
                //Suicide if planet destroyed :)
                Destroy();
                
                return 0;
            }

            var tr = transform;
            
            var step =  enemyWaveData.Speed * Time.deltaTime;
            tr.position = Vector3.MoveTowards(tr.position, targetTransform.position, step);

            return 0;
        }
    }
}