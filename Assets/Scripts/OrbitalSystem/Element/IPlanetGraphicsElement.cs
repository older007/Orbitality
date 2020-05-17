using Boo.Lang;
using UnityEngine;

namespace OrbitalSystem.Element
{
    public interface IPlanetGraphicsElement
    {
        void SetPlanetGroundColor(Color color);
        void SetPlanetWaterColor(Color color);
        void SetMaterialParameter(float param);
        void SetAtmosphereColor(Color color);
    }
}