namespace DAL.Entities;
using DAL.Interfaces;

public class Bird : Animal, IActionable
{
    public Bird(string name) : base(name) { }

    public string Run()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб бігати.";
        return $"{Name} швидко перебирає лапками!";
    }

    public string Sing()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб співати.";
        return $"{Name} співає чудову пісню!";
    }

    public string Fly()
    {
        if (!CanDoEnergeticActions()) return $"{Name} занадто голодний щоб летіти.";
        return $"{Name} злітає у небо!";
    }

    public string Crawl() => $"{Name} — птах, повзати не вміє.";
}