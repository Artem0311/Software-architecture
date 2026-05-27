namespace DAL.Entities;
using DAL.Interfaces;

public class Bird : Animal, IActionable
{
    public Bird(string name) : base(name) { }

    public string Run()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може бігати!";
        SpendEnergy();
        return $"{Name} біжить на лапках!";
    }

    public string Sing()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може співати!";
        SpendEnergy();
        return $"{Name} красиво співає!";
    }

    public string Fly()
    {
        if (IsHungry) return $"[ВІДМОВА] {Name} голодний і не може літати!";
        SpendEnergy();
        return $"{Name} злітає у небо!";
    }

    public string Crawl() => $"{Name} не вміє повзати.";
}