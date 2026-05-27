namespace DAL.Events;

public class AnimalDiedEventArgs : EventArgs
{
    public string AnimalName { get; }
    public string AnimalType { get; }
    public string Reason { get; }

    public AnimalDiedEventArgs(string animalName, string animalType, string reason)
    {
        AnimalName = animalName;
        AnimalType = animalType;
        Reason = reason;
    }
}