namespace DAL.Entities;
using DAL.Interfaces;

public class Cat : Animal, IActionable
{
    public Cat(string name) : base(name) { }

    public string Run()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може бігати!";
        SpendEnergy();
        return $"{Name} стрімко біжить!";
    }

    public string Sing()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може муркотіти!";
        SpendEnergy();
        return $"{Name} муркоче.";
    }

    public string Fly() => $"{Name} не вміє літати.";

    public string Crawl() => $"{Name} тихо повзе.";
}