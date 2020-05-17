using BattleSystem;
using Utils;

namespace OrbitalSystem.Weapon
{
    public interface IRocketView : IMonoView<RocketData>
    {
        void UpdateCallDown(float currentCallDown);
    }
}