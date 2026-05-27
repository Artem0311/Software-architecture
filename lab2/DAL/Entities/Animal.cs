namespace DAL.Entities;
using DAL.Events;

public abstract class Animal
{
    public string Name { get; }
    public bool IsHungry { get; private set; }
    public bool IsHappy { get; private set; }
    public bool IsAlive { get; private set; } = true;

    private int _feedingsToday = 0;
    private int _hoursSinceLastFeed = 0;

    // Observer: події які генерує тварина
    public event EventHandler<AnimalHungryEventArgs>? BecameHungry;
    public event EventHandler<AnimalDiedEventArgs>? Died;

    protected Animal(string name)
    {
        Name = name;
    }

    public string Feed()
    {
        if (!IsAlive) return $"{Name} вже не живе.";
        if (_feedingsToday >= 5) return $"{Name} вже наївся сьогодні!";

        _feedingsToday++;
        IsHungry = false;
        _hoursSinceLastFeed = 0;
        return $"{Name} поїв. Годувань сьогодні: {_feedingsToday}/5";
    }

    public string AdvanceHours(int hours)
    {
        if (!IsAlive) return $"{Name} вже не живе.";

        _hoursSinceLastFeed += hours;

        if (_hoursSinceLastFeed > 8 && !IsHungry)
        {
            IsHungry = true;
            BecameHungry?.Invoke(this, new AnimalHungryEventArgs(Name, GetType().Name));
        }

        if (_hoursSinceLastFeed > 24 && _feedingsToday == 0)
        {
            IsAlive = false;
            Died?.Invoke(this, new AnimalDiedEventArgs(Name, GetType().Name, "голод"));
            return $"{Name} загинув від голоду.";
        }

        return $"{Name}: минуло {hours} год. Без їжі: {_hoursSinceLastFeed} год.";
    }

    public void MakeHappy() => IsHappy = true;
    public void MakeUnhappy() => IsHappy = false;
    public bool CanDoEnergeticActions() => IsAlive && !IsHungry;
}