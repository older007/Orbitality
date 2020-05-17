namespace OrbitalSystem.Element
{
    public interface IElementMover<in T>
    {
        void Init(T data);
        void MoveAndRotate();
    }
}