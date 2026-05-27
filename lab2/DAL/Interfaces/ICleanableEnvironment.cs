namespace DAL.Interfaces;

public interface ICleanableEnvironment : IEnvironment
{
    bool NeedsCleaning { get; }
    string Clean();
    void MakeDirty();
}