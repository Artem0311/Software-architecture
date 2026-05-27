namespace AnimalSimulation.Domain;
public class Dog : Animal, IActionable
{
    public Dog(string name) : base(name) {}

    public string PerformAction()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} занадто голодний, щоб бігати! Погодуйте його.";
        
        SpendEnergy(); 
        return $"{Name} радісно бігає і грається! (Тепер він зголоднів)";
    }
}

public class Bird : Animal, IActionable
{
    public Bird(string name) : base(name) {}

    public string PerformAction()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} занадто голодний, щоб літати! Погодуйте його.";
        
        SpendEnergy();
        return $"{Name} високо літає у небі! (Тепер він зголоднів)";
    }
}