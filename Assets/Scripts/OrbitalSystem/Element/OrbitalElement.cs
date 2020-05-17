using System;
using DefaultNamespace;
using UnityEngine;

namespace OrbitalSystem.Element
{
    public class OrbitalElement : MonoBase, IOrbitalSetup
    {
        private IOrbitalElementUi orbitalElementUi;
        private IOrbitalElementPhysics planetPhisics;
        private IPlanetGraphicsElement planetGraphics;
        private IElementMover<OrbitalModel> planteMover;
        public OrbitalModel ElementModel { get; private set; }
        public bool IsSetup { get; private set; } = false;

        public event Action<OrbitalModel> OnDataUpdated 
        {
            add => dataupdated += value;
            remove => dataupdated -= value;
        }

        private Action<OrbitalModel> dataupdated;
        
        public event Action<OrbitalElement> OnDestroyed 
        {
            add => onDestroyed += value;
            remove => onDestroyed -= value;
        }

        private Action<OrbitalElement> onDestroyed;

        private void Awake()
        {
            orbitalElementUi = GetComponentInChildren<IOrbitalElementUi>();
            planetPhisics = GetComponentInChildren<IOrbitalElementPhysics>();
            planetGraphics = GetComponentInChildren<IPlanetGraphicsElement>();
            planteMover = GetComponentInChildren<IElementMover<OrbitalModel>>();
        }

        public void DestroyElement()
        {
            orbitalElementUi.Destroy();
            
            OnDestroy();
        }

        protected override void OnUpdate()
        {
            if (!IsSetup)
            {
                return;
            }

            planteMover.MoveAndRotate();
            
        }

        public void LoadSetup(OrbitalModel orbitalModel)
        {
            IsSetup = true;

            ElementModel = orbitalModel;
        
            LoadParams(ElementModel);
        }

        public void SetupLayer(int layer)
        {
            planetPhisics.SetLayer(layer);
        }

        private void LoadParams(OrbitalModel orbitalModel)
        {
            planteMover.Init(orbitalModel);
            orbitalElementUi.Init(transform);

            orbitalElementUi.UpdateData(orbitalModel);
            
            dataupdated += orbitalElementUi.UpdateData;
            planetPhisics.CollisionEnter += CollisionEnter;
            planetPhisics.TriggerEnter += TriggerEnter;
            
            planetGraphics.SetAtmosphereColor(orbitalModel.AtmosphereColor);
            planetGraphics.SetMaterialParameter(orbitalModel.GroundParam);
            planetGraphics.SetPlanetGroundColor(orbitalModel.GroundColor);
            planetGraphics.SetPlanetWaterColor(orbitalModel.WaterColor);
        }

        private void TriggerEnter(IGravity rocket)
        {
            rocket.AddGravitation(transform.position);
        }

        private void CollisionEnter(IDamageElementBase damageElement)
        {
            ElementModel.HealthPoint -= damageElement.Damage;

            damageElement.Destroy();
            
            if (ElementModel.HealthPoint <= 0)
            {
                DestroyElement();
                onDestroyed?.Invoke(this);
            }

            dataupdated?.Invoke(ElementModel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (orbitalElementUi != null)
            {
                dataupdated -= orbitalElementUi.UpdateData;
            }
            
            planetPhisics.CollisionEnter -= CollisionEnter;
            planetPhisics.TriggerEnter -= TriggerEnter;
        }
    }
}
