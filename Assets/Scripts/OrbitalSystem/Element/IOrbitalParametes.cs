namespace OrbitalSystem.Element
{
    public interface IOrbitalSetup
    {
        OrbitalModel ElementModel { get; }
        bool IsSetup { get; }
        void LoadSetup(OrbitalModel orbitalModel);
        void SetupLayer(int layer);
    }
}