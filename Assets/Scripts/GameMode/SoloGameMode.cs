using OrbitalSystem;
using OrbitalSystem.Element;

namespace GameMode
{
    public class SoloGameMode : GameMode
    {
        private OrbitalManager orbitalManager => ServiceLocator.Get<OrbitalManager>();

        protected override string CompleteMessage => "You destroyed solar system";
        protected override string ModeName => "Destroy solar system";


        public override void OnPlanetDestroyed(OrbitalModel planet)
        {
            if (orbitalManager.PlanetsCount == 1)
            {
                CompleteGame();
            }
        }
    }
}