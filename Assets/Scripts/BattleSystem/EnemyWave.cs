namespace BattleSystem
{
    public class EnemyWave
    {
        public int Count { get; }
        public float Hp { get; set; }
        public int CallDown { get; }
        public int Damage { get; }
        public float Speed { get; }
        public float BulletDamage { get; }
        
        public EnemyWave(EnemyWaveData data, int multiplier)
        {
            Count = data.Count * multiplier;
            Hp = data.Hp * multiplier;
            CallDown = data.CallDown * multiplier;
            Damage = data.Damage * multiplier;
            Speed = data.Speed * multiplier;
            BulletDamage = data.BulletDamage * multiplier;
        }
    }
}