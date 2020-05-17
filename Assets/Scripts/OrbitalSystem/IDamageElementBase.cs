namespace OrbitalSystem
{
    public interface IDamageElementBase
    {
        float Damage { get; }
        bool Destroyed { get; }
        void Destroy();
        float Move();
    }
}