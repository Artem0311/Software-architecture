namespace DAL.Factories;
using DAL.Entities;

// Factory Method: абстрактна фабрика — шаблон для створення тварин
public abstract class AnimalFactory
{
    public abstract Animal CreateAnimal(string name);
}