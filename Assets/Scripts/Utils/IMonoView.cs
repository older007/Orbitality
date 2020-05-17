namespace Utils
{
    public interface IMonoView<in T>
    {
        void SetData(T data);
        void UpdateData(T data);
    }
    
    public interface IMonoView<in T, in V>
    {
        void SetData(T data1, V data2);
        void UpdateData(T data);
    }
}