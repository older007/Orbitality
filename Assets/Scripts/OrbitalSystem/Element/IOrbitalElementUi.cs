using UnityEngine;

namespace OrbitalSystem.Element
{
    public interface IOrbitalElementUi
    {
        void Init(Transform target);
        void UpdateData(OrbitalModel model);
        void Destroy();
    }
}