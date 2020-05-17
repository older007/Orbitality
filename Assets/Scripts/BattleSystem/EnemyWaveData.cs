using UnityEngine;

namespace BattleSystem
{    
    [CreateAssetMenu(fileName = "WaveData", menuName = "Orbital/CreateWaveData", order = 1)]
    public class EnemyWaveData : ScriptableObject
    {
        public int Count => count;
        public float Hp => hp;
        public int CallDown => callDown;
        public int Damage => damage;
        public float Speed => speed;
        public float BulletDamage => bulletDamage;
        
        [SerializeField, Min(1)] private int count;
        [SerializeField, Min(1)] private float hp;
        [SerializeField, Min(1)] private int callDown;
        [SerializeField, Min(1)] private int damage;
        [SerializeField, Min(1)] private int speed;
        [SerializeField, Min(1)] private int bulletDamage;
    }
}