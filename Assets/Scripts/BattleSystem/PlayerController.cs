using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using OrbitalSystem;
using OrbitalSystem.Weapon;
using UnityEngine;
using Utils;

namespace BattleSystem
{
    public class PlayerController : MonoBase
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private List<RocketData> rocketsData;
        [SerializeField] private Rocket damageElementPrefab;
        [SerializeField] private MeshCollider clickZone;
        
        private Dictionary<int, RocketView> rocketdictionary = new Dictionary<int, RocketView>();
        private Dictionary<int, float> rocketCalldown = new Dictionary<int, float>();
        private List<RocketView> rocketViews = new List<RocketView>();
        private List<Rocket> rocketOnScene = new List<Rocket>();
        private bool isInited;
        private int currentRocket = 0;

        private RocketData CurrentRocket => rocketsData[currentRocket];
        private RocketView CurrentView => rocketdictionary[currentRocket];
        private Vector3 rocketStartPos => ServiceLocator.Get<OrbitalManager>().PlayerElement.transform.position;
        
        public void Init()
        {
            isInited = true;
            
            rocketViews = playerView.CreateViews(rocketsData);
            
            SubscribeViews();
        }

        private void SubscribeViews()
        {
            for (var i = 0; i < rocketViews.Count; i++)
            {
                var view = rocketViews[i];
                
                rocketdictionary.Add(i, view);
                rocketCalldown.Add(i, 0);
            }
        }

        protected override void OnUpdate()
        {
            if (!isInited)
            {
                return;
            }

            ChangeCallDown();
            ChoseWeapon();
            Shot();

            for (var i = 0; i < rocketOnScene.Count; i++)
            {
                var rocket = rocketOnScene[i];
                if (rocket == null)
                {
                    continue;
                }

                if (Mathf.Abs(rocket.Move()) > 600)
                {
                    rocketOnScene.RemoveAt(i);
                    rocket.Destroy();
                }
            }
        }

        private void Shot()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0) || rocketCalldown[currentRocket] != 0)
            {
                return;
            }
            
            rocketCalldown[currentRocket] = CurrentRocket.CallDown;
            
            CreateRocket();
        }

        private void CreateRocket()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(clickZone.Raycast(ray, out var hitData, 1000))
            {
                var targetPosition = hitData.point;
                var rocket = Instantiate(damageElementPrefab, rocketStartPos, Quaternion.identity);

                var newPosition = targetPosition - rocketStartPos;
                
                rocket.Init(CurrentRocket, newPosition);
                rocketOnScene.Add(rocket);
            }
        }

        private void ChangeCallDown()
        {
            var values = rocketCalldown.Select(s => s.Value).ToList();

            for (var i = 0; i < values.Count(); i++)
            {
                var value = values[i];
                var callDown = Mathf.Clamp(value -= Time.deltaTime, 0f, 1000f);

                rocketCalldown[i] = callDown;
                
                rocketdictionary[i].UpdateCallDown(callDown);
            }
        }

        private void ChoseWeapon()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                currentRocket = 0;
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                currentRocket = 1;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                currentRocket = 2;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Load Rocket Data")]
        private void LoadRocketDataFromResources()
        {
            rocketsData = Resources.LoadAll<RocketData>(EditorConstants.RocketFolder).ToList();
        }
#endif
    }
}