using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Rocket", menuName = "Orbital/CreateRocketData", order = 1)]
    public class RocketData : ScriptableObject
    {
        public Sprite Icon => icon;
        public float Damage => damage;
        public float Speed => speed;
        public float CallDown => callDown;
        public float Weight => weight;

        [SerializeField] private Sprite icon;
        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private float callDown;
        [SerializeField] private float weight;
    }
}