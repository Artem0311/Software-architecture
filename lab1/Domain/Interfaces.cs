namespace AnimalSimulation.Domain;
public interface IActionable 
{ 
    string PerformAction(); 
}

public interface IEnvironment 
{ 
    List<Animal> Animals { get; }
    void AddAnimal(Animal animal); 
}

public interface ICleanableEnvironment : IEnvironment 
{ 
    bool NeedsCleaning { get; }
    string Clean();
    void MakeDirty();
}