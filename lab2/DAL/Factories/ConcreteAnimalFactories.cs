namespace DAL.Factories;
using DAL.Entities;

public class CatFactory : AnimalFactory
{
    public override Animal CreateAnimal(string name) => new Cat(name);
}

public class DogFactory : AnimalFactory
{
    public override Animal CreateAnimal(string name) => new Dog(name);
}

public class BirdFactory : AnimalFactory
{
    public override Animal CreateAnimal(string name) => new Bird(name);
}

public class SnakeFactory : AnimalFactory
{
    public override Animal CreateAnimal(string name) => new Snake(name);
}