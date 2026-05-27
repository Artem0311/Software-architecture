namespace DAL.Entities;
using DAL.Interfaces;

public class Snake : Animal, IActionable
{
    public Snake(string name) : base(name) { }

    public string Run() => $"{Name} не вміє бігати.";

    public string Sing() => $"{Name} не вміє співати.";

    public string Fly() => $"{Name} не вміє літати.";

    public string Crawl()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може повзати швидко!";
        SpendEnergy();
        return $"{Name} плавно повзе.";
    }
}