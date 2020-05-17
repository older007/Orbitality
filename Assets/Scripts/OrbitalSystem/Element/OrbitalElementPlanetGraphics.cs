using Boo.Lang;
using UnityEngine;

namespace OrbitalSystem.Element
{
    public class OrbitalElementPlanetGraphics : MonoBehaviour, IPlanetGraphicsElement
    {
        [SerializeField] private Renderer planetRenderer;
        [SerializeField] private Renderer atmosphereRenderer;

        private Material planetMaterial => planetRenderer.material;
        private Material atmosphereMaterial => atmosphereRenderer.material;
        
        public void SetPlanetGroundColor(Color color)
        {
            planetMaterial.SetColor("_ground", color);
        }

        public void SetPlanetWaterColor(Color color)
        {
            planetMaterial.SetColor("_water", color);
        }

        public void SetMaterialParameter(float param)
        {
            planetMaterial.SetFloat("_param", param);
        }

        public void SetAtmosphereColor(Color color)
        {
            atmosphereMaterial.SetColor("_color", color);
        }
    }
}