using System;

namespace OrbitalSystem.Element
{
    public interface IOrbitalElementPhysics
    {
        void SetLayer(int layer);
        event Action<IGravity> TriggerEnter;
        event Action<IDamageElementBase> CollisionEnter;
    }
}