namespace DAL.Entities;

public abstract class Animal
{
    public string Name { get; }
    public bool IsHungry { get; private set; }
    public bool IsHappy { get; private set; }
    private int _feedingsToday = 0;

    protected Animal(string name)
    {
        Name = name;
        IsHungry = false;
        IsHappy = false;
    }

    public string Feed()
    {
        if (_feedingsToday >= 5)
            return $"{Name} вже наївся достатньо сьогодні!";

        _feedingsToday++;
        IsHungry = false;
        return $"{Name} поїв. Годувань сьогодні: {_feedingsToday}/5";
    }

    public void SpendEnergy()
    {
        IsHungry = true;
    }

    public void ResetDay()
    {
        _feedingsToday = 0;
        IsHungry = true; // з нового дня тварина голодна
    }

    public void MakeHappy() => IsHappy = true;
    public void MakeUnhappy() => IsHappy = false;
}