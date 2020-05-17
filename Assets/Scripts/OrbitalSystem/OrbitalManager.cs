using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OrbitalSystem.Element;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace OrbitalSystem
{
    public class OrbitalManager : MonoBehaviour
    {
        [SerializeField] private OrbitalElement orbitalElementPrefab;
        [SerializeField] private Transform orbitalElementRoot;
        [SerializeField] private GamePlaySceneViewController gamePlaySceneViewController;
        
        private List<OrbitalModel> orbitalModels = new List<OrbitalModel>();
        private List<OrbitalElement> orbitalElements = new List<OrbitalElement>();
        public OrbitalElement PlayerElement => orbitalElements.Find(f=>f.ElementModel.Owner == Owner.Player);
        public int PlanetsCount => orbitalElements.Count(c => c != null);
        public OrbitalModel playerPlanet => PlayerElement.ElementModel;
        
        public event Action<OrbitalModel> OnPlanetDestroyed 
        {
            add => onPlanetDestroyed += value;
            remove => onPlanetDestroyed -= value;
        }

        private Action<OrbitalModel> onPlanetDestroyed;
        
        private void Awake()
        {
            ServiceLocator.Add(this);
            
            CreateSolarSystem();
        }

        private void CreateSolarSystem()
        {
            if (HaveSaveState())
            {
                orbitalModels = LoadElements();
                
                LoadElementsFromSave(orbitalModels);
            }
            else
            {
                CreateElements();
            }
            
            SetupPlayerData();
        }

        private void SetupPlayerData()
        {
            PlayerElement.SetupLayer(9);
        }

        private List<OrbitalModel> LoadElements()
        {
            var json = PlayerPrefs.GetString(Constants.GameData);
            var data = JsonConvert.DeserializeObject<List<OrbitalModel>>(json);

            return data;
        }

        private List<OrbitalElement> LoadElementsFromSave(List<OrbitalModel> data)
        {
            foreach (var model in data)
            {
                var orbitalElement = CreateOrbitalElement(model);

                orbitalElements.Add(orbitalElement);
            }

            return orbitalElements;
        }

        private void CreateElements()
        {
            float lastSize = 15;
            float lastPosition = 0;
            
            for (var i = 0; i < Constants.PlanetsCount; i++)
            {
                var elementModel = CreateElementData(ref lastSize, ref lastPosition);

                if (i == 2)
                {
                    elementModel.Owner = Owner.Player;
                }

                var orbitalElement = CreateOrbitalElement(elementModel);
                
                orbitalModels.Add(elementModel);
                orbitalElements.Add(orbitalElement);
            }
        }

        private OrbitalModel CreateElementData(ref float lastSize, ref float lastPosition)
        {
            var size = Random.Range(8f, 20f);
            var sizeAll = size * 3;
            var radiusAll = sizeAll / 2;
            var delta = Random.Range(0f, 3f);

            var radius = lastPosition + lastSize + radiusAll + delta;

            lastSize = radiusAll;
            lastPosition = radius;
            
            var model = new OrbitalModel()
            {
                HealthPoint = Random.Range(0,1001),
                RotateSpeed = Random.Range(-25f,25f),
                Size = new Vector3(size, size, size),
                Radius = radius,
                MoveSpeed = Random.Range(1f, 10f),
                Position = new Vector3(radius, 0 , 0),
                WaterColor = Random.ColorHSV(),
                GroundColor = Random.ColorHSV(),
                AtmosphereColor = Random.ColorHSV(),
                GroundParam = Random.Range(1f,50f)
            };
            
            return model;
        }

        private OrbitalElement CreateOrbitalElement(OrbitalModel data)
        {
            var element = Instantiate(orbitalElementPrefab, orbitalElementRoot);

            element.LoadSetup(data);
            element.OnDestroyed += ElementOnOnDestroyed;
            
            return element;
        }

        private void ElementOnOnDestroyed(OrbitalElement obj)
        {
            orbitalModels.Remove(obj.ElementModel);
            orbitalElements.Remove(obj);
            obj.DestroyElement();
            onPlanetDestroyed?.Invoke(obj.ElementModel);
            
            Destroy(obj.gameObject);
        }

        private bool HaveSaveState()
        {
            if (PlayerPrefs.GetInt(Constants.IsSaved, 0) == 0)
            {
                return false;
            }

            return true;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(orbitalModels, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
            Debug.LogWarning(json);
            PlayerPrefs.SetInt(Constants.IsSaved, 1);
            PlayerPrefs.SetString(Constants.GameData, json);
        }

        public void PlayerSubscribe(Action<OrbitalModel> action)
        {
            PlayerElement.OnDataUpdated += action;
        }
        
        public void SetupEnemyData()
        {
            foreach (var element in orbitalElements)
            {
                element.SetupLayer(9);
            }
        }

        public Transform GetTarget()
        {
            if (orbitalElements.Count == 0)
            {
                return null;
            }

            var sorted = orbitalElements.Where(s => s != null);
            var element = sorted.ElementAt(Random.Range(0, sorted.Count()));

            return element.transform;
        }
    }
}
