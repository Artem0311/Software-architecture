namespace DAL.Events;

public class AnimalHungryEventArgs : EventArgs
{
    public string AnimalName { get; }
    public string AnimalType { get; }

    public AnimalHungryEventArgs(string animalName, string animalType)
    {
        AnimalName = animalName;
        AnimalType = animalType;
    }
}