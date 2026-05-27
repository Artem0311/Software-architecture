namespace DAL.Factories;
using DAL.Entities;

public abstract class AnimalFactory
{
    public abstract Animal CreateAnimal(string name);
}