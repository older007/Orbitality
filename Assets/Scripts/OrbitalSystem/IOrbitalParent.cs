using UnityEngine;

namespace OrbitalSystem
{
    public interface IOrbitalParent
    {
        string SystemName { get; }
        Vector3 OrbitalCenter { get; }
        Vector3 GlobalOrbitalCenter { get; }
    }
}