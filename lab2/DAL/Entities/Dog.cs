namespace DAL.Entities;
using DAL.Interfaces;

public class Dog : Animal, IActionable
{
    public Dog(string name) : base(name) { }

    public string Run()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб бігати.";
        return $"{Name} мчить з усіх ніг!";
    }

    public string Sing()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб гавкати.";
        return $"{Name} гавкає: Гав-гав!";
    }

    public string Fly() => $"{Name} — собака, літати не вміє.";

    public string Crawl() => $"{Name} повзе по-пластунськи.";
}