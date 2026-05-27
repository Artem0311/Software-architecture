namespace DAL.Entities;
using DAL.Interfaces;

public class Snake : Animal, IActionable
{
    public Snake(string name) : base(name) { }

    public string Run() => $"{Name} — змія, бігати не вміє.";

    public string Sing()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб шипіти.";
        return $"{Name} шипить: Шшшш!";
    }

    public string Fly() => $"{Name} — змія, літати не вміє.";

    public string Crawl() => $"{Name} плавно повзе.";
}