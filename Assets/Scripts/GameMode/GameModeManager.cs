using System;
using OrbitalSystem;
using TMPro;
using UnityEngine;
using Utils;

namespace GameMode
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField] private GameModeView gameModeView;
        private OrbitalManager orbitalManager => ServiceLocator.Get<OrbitalManager>();
        
        private GameMode _gameGameMode;

        public void StartGame()
        {
            LoadGameMode();

            _gameGameMode.Init(gameModeView);
            
            orbitalManager.OnPlanetDestroyed += _gameGameMode.OnPlanetDestroyed;
            
            _gameGameMode.ShowGameMode();
        }

        public void Save()
        {
            _gameGameMode.Save();   
        }

        private void LoadGameMode()
        {
            switch (PlayerPrefs.GetInt(Constants.GameMode, 0))
            {
                case 1:
                    _gameGameMode = new SoloGameMode();
                    break;
                case 2:
                    _gameGameMode = new PveGameMode();
                    break;
                
                case 0:
                    
                    break;
            }
        }
    }
}