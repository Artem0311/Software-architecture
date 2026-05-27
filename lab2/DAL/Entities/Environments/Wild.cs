namespace DAL.Entities.Environments;
using DAL.Interfaces;
using DAL.Entities;

public class Wild : IEnvironment
{
    public List<Animal> Animals { get; } = new();

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
        animal.MakeHappy();
    }
}