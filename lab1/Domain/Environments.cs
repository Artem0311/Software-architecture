namespace AnimalSimulation.Domain;
public class Owner : ICleanableEnvironment
{
    public List<Animal> Animals { get; } = new();
    public bool NeedsCleaning { get; private set; } = false;

    public void AddAnimal(Animal animal)
    {
        if (Animals.Count >= 1) 
            throw new InvalidOperationException("У Хазяїна може бути лише 1 тварина!");
        
        Animals.Add(animal);
        MakeDirty();
    }

    public void MakeDirty() => NeedsCleaning = true;

    public string Clean()
    {
        NeedsCleaning = false;
        return "Дім Хазяїна тепер чистий!";
    }
}

public class PetShop : ICleanableEnvironment
{
    public List<Animal> Animals { get; } = new();
    public bool NeedsCleaning { get; private set; } = false;

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
        MakeDirty();
    }

    public void MakeDirty() => NeedsCleaning = true;
    public string Clean()
    {
        NeedsCleaning = false;
        return "У Зоомагазині прибрано!";
    }
}

public class Wild : IEnvironment
{
    public List<Animal> Animals { get; } = new();
    public void AddAnimal(Animal animal) => Animals.Add(animal);
}