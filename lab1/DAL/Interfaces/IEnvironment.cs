namespace DAL.Interfaces;
using DAL.Entities;

public interface IEnvironment
{
    List<Animal> Animals { get; }
    void AddAnimal(Animal animal);
}