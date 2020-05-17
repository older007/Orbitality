using BattleSystem;
using OrbitalSystem;
using OrbitalSystem.Element;
using UnityEngine;
using Utils;

namespace GameMode
{
    public class PveGameMode : GameMode
    {
        private OrbitalManager orbitalManager => ServiceLocator.Get<OrbitalManager>();

        private EnemyManager enemyManager;
        
        protected override string CompleteMessage => "Aliens destroyed you planet...";
        protected override string ModeName => "Save solar system Mode";
        
        public override void Init(GameModeView view)
        {
            base.Init(view);
            
            orbitalManager.SetupEnemyData();
            CreateEnemyManager();
        }
        
        public override void OnPlanetDestroyed(OrbitalModel planet)
        {
            if (planet.Owner == Owner.Player)
            {
                CompleteGame();
            }
        }

        private void CreateEnemyManager()
        {
            var gm = new GameObject("Enemy manager");

            enemyManager = gm.AddComponent<EnemyManager>();

            var savedData = PlayerPrefs.GetInt(Constants.PveSave, 1);

            Debug.LogWarning(savedData);
            
            enemyManager.Init(savedData);
        }

        public override void Save()
        {
            base.Save();
            
            PlayerPrefs.SetInt(Constants.PveSave, enemyManager.CurrentWave);
        }
    }
}