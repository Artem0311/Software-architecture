namespace AnimalSimulation.Infrastructure;
using System.Linq;
using AnimalSimulation.Domain;

public class ConsoleMenu
{
    private readonly List<IEnvironment> _envs = new() { new Owner(), new PetShop(), new Wild() };

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("СИМУЛЯЦІЯ ТВАРИН");
            Console.WriteLine("1. Додати тварину");
            Console.WriteLine("2. Взаємодіяти (Грати / Годувати)");
            Console.WriteLine("3. Прибрати середовища");
            Console.WriteLine("4. Статус системи");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");
            
            string choice = Console.ReadLine() ?? "";
            
            try {
                if (choice == "1") AddAnimal();
                else if (choice == "2") Interact();
                else if (choice == "3") CleanAll();
                else if (choice == "4") ShowStatus();
                else if (choice == "0") break;
            } catch (Exception ex) {
                Console.WriteLine($"\n[ПОМИЛКА БІЗНЕС-ЛОГІКИ] {ex.Message}");
            }
        }
    }

    private void AddAnimal()
    {
        Console.WriteLine("\nКуди додати? 1. Хазяїн, 2. Магазин, 3. Воля");
        int envIdx = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("Хто це? 1. Собака, 2. Птах");
        string type = Console.ReadLine() ?? "1";
        
        Console.WriteLine("Введіть ім'я:");
        string name = Console.ReadLine() ?? "Pet";

        Animal animal = type == "2" ? new Bird(name) : new Dog(name);
        
        _envs[envIdx].AddAnimal(animal);
        Console.WriteLine($"\n[УСПІХ] {name} успішно додано!");
    }

    private void Interact()
    {
        var allAnimals = _envs.SelectMany(e => e.Animals).ToList();
        if (allAnimals.Count == 0) { Console.WriteLine("\nТварин ще немає!"); return; }

        Console.WriteLine("\nСписок тварин:");
        for (int i = 0; i < allAnimals.Count; i++)
            Console.WriteLine($"{i}. {allAnimals[i].Name} ({allAnimals[i].GetType().Name}) - Голодний: {allAnimals[i].IsHungry}");

        Console.Write("Оберіть номер тварини: ");
        int idx = int.Parse(Console.ReadLine() ?? "0");
        var pet = allAnimals[idx];

        Console.WriteLine("\nЩо робити? 1. Погодувати, 2. Гратися (Дія)");
        string act = Console.ReadLine() ?? "1";

        if (act == "1") Console.WriteLine("\n" + pet.Feed());
        else if (act == "2" && pet is IActionable actionablePet) 
        {
            Console.WriteLine("\n" + actionablePet.PerformAction());
        }
    }

    private void CleanAll()
    {
        Console.WriteLine();
        foreach (var env in _envs)
        {
            if (env is ICleanableEnvironment cleanable)
                Console.WriteLine(cleanable.Clean());
        }
    }

    private void ShowStatus()
    {
        Console.WriteLine("СТАТУС");
        foreach(var env in _envs)
        {
            string dirtyStatus = env is ICleanableEnvironment c && c.NeedsCleaning ? "[ПОТРЕБУЄ ПРИБИРАННЯ]" : "[ЧИСТО]";
            if (env is Wild) dirtyStatus = "[ПРИРОДА]";
            Console.WriteLine($"{env.GetType().Name}: Тварин - {env.Animals.Count}. {dirtyStatus}");
        }
    }
}