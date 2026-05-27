namespace DAL.Entities;
using DAL.Interfaces;

public class Cat : Animal, IActionable
{
    public Cat(string name) : base(name) { }

    public string Run()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб бігати.";
        return $"{Name} біжить!";
    }

    public string Sing()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб нявкати.";
        return $"{Name} нявкає: Няяяв!";
    }

    public string Fly() => $"{Name} — кіт, літати не вміє.";

    public string Crawl() => $"{Name} повзе крадькома.";
}