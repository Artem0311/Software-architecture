namespace DAL.Entities.Environments;
using DAL.Interfaces;
using DAL.Entities;

public class Owner : ICleanableEnvironment
{
    public string OwnerName { get; }
    public List<Animal> Animals { get; } = new();
    public bool NeedsCleaning { get; private set; } = false;

    public Owner(string ownerName)
    {
        OwnerName = ownerName;
    }

    public void AddAnimal(Animal animal)
    {
        if (Animals.Count >= 1)
            throw new InvalidOperationException("У хазяїна може бути лише 1 тварина!");
        
        Animals.Add(animal);
        MakeDirty();
    }

    public void MakeDirty() => NeedsCleaning = true;

    public string Clean()
    {
        if (!NeedsCleaning)
            return $"У {OwnerName} і так чисто!";
        
        NeedsCleaning = false;
        foreach (var animal in Animals)
            animal.MakeHappy();
        
        return $"{OwnerName} прибрав(ла). Тварина щаслива!";
    }
}