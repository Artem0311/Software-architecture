namespace DAL.Entities;
using DAL.Interfaces;

public class Dog : Animal, IActionable
{
    public Dog(string name) : base(name) { }

    public string Run()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може бігати!";
        SpendEnergy();
        return $"{Name} радісно бігає!";
    }

    public string Sing()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може співати!";
        SpendEnergy();
        return $"{Name} голосно гавкає!";
    }

    public string Fly() => $"{Name} не вміє літати.";

    public string Crawl() => $"{Name} повзе по підлозі.";
}