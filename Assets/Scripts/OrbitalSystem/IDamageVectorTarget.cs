using UnityEngine;

namespace OrbitalSystem
{
    public interface IDamageVectorTarget<in T> : IDamageElementBase
    {
        void Init(T data, Vector3 targetPos);
    }
    
    public interface IDamageTransformTarget<in T> : IDamageElementBase
    {
        void Init(T data, Transform targetPos);
    }
}