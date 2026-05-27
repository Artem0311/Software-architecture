namespace AnimalSimulation.Domain;
public abstract class Animal
{
    public string Name { get; }
    public bool IsHungry { get; private set; }

    protected Animal(string name)
    {
        Name = name;
        IsHungry = false; 
    }

    public string Feed()
    {
        IsHungry = false;
        return $"{Name} смачно поїв і більше не голодний.";
    }

    public void SpendEnergy()
    {
        IsHungry = true; 
    }
}