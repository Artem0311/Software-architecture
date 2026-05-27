namespace BLL.Observers;
using DAL.Events;

public class HungerNotifier
{
    private readonly string _ownerName;

    public HungerNotifier(string ownerName)
    {
        _ownerName = ownerName;
    }

    public void OnAnimalHungry(object? sender, AnimalHungryEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[СПОВІЩЕННЯ] {_ownerName}, тварина {e.AnimalName} голодна!");
        Console.ResetColor();
    }

    public void OnAnimalDied(object? sender, AnimalDiedEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[СПОВІЩЕННЯ] {_ownerName}, тварина {e.AnimalName} загинула!");
        Console.ResetColor();
    }
}