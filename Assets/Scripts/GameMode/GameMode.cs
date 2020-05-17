using OrbitalSystem.Element;
using UnityEngine;
using Utils;

namespace GameMode
{
    public abstract class GameMode
    {
        private UpdateProvider UpdateProvider => ServiceLocator.Get<UpdateProvider>();
        private DelayCallService DelayCallService => ServiceLocator.Get<DelayCallService>();
        
        protected GameModeView GameModeView;
        protected abstract string CompleteMessage { get; }
        protected abstract string ModeName { get; }

        public virtual void Save()
        {
            
        }

        protected virtual void Load()
        {
            
        }

        public virtual void Init(GameModeView view)
        {
            GameModeView = view;
        }

        public void ShowGameMode()
        {
            GameModeView.SetData(ModeName, OnExit);
        }

        public virtual void CompleteGame()
        {
            UpdateProvider.IsStoped = true;
            GameModeView.UpdateData(CompleteMessage);
            DelayCallService.Dispose();

            PlayerPrefs.DeleteAll();
        }

        public abstract void OnPlanetDestroyed(OrbitalModel planet);

        protected virtual void OnExit()
        {
            GameLoader.Reload();
        }
    }
}