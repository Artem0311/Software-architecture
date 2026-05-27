namespace PL;
using BLL;
using DAL.Entities;

public class ConsoleMenu
{
    private readonly AnimalService _service = new();

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n===== СИМУЛЯЦІЯ ТВАРИН =====");
            Console.WriteLine("1. Додати тварину");
            Console.WriteLine("2. Годувати тварину");
            Console.WriteLine("3. Виконати дію з твариною");
            Console.WriteLine("4. Прибрати середовище");
            Console.WriteLine("5. Випустити тварину на волю");
            Console.WriteLine("6. Статус системи");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine() ?? "";

            try
            {
                if (choice == "1") AddAnimal();
                else if (choice == "2") FeedAnimal();
                else if (choice == "3") PerformAction();
                else if (choice == "4") CleanEnvironment();
                else if (choice == "5") MoveToWild();
                else if (choice == "6") Console.WriteLine("\n" + _service.GetStatus());
                else if (choice == "0") break;
                else Console.WriteLine("Невірний вибір!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА] {ex.Message}");
            }
        }
    }

    private void AddAnimal()
    {
        Console.WriteLine("\nКуди додати тварину?");
        Console.WriteLine("1. Хазяїн");
        Console.WriteLine("2. Зоомагазин");
        Console.WriteLine("3. Воля");
        Console.Write("Оберіть: ");
        int envIndex = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("\nОберіть тварину:");
        Console.WriteLine("1. Собака");
        Console.WriteLine("2. Кіт");
        Console.WriteLine("3. Птах");
        Console.WriteLine("4. Змія");
        Console.Write("Оберіть: ");
        string type = Console.ReadLine() ?? "1";

        Console.Write("Введіть ім'я: ");
        string name = Console.ReadLine() ?? "Тварина";

        Animal animal = type switch
        {
            "1" => new Dog(name),
            "2" => new Cat(name),
            "3" => new Bird(name),
            "4" => new Snake(name),
            _ => new Dog(name)
        };

        Console.WriteLine("\n" + _service.AddAnimal(animal, envIndex));
    }

    private void FeedAnimal()
    {
        var animals = _service.GetAllAnimals();
        if (animals.Count == 0)
        {
            Console.WriteLine("\nТварин ще немає!");
            return;
        }

        Console.WriteLine("\nОберіть тварину:");
        for (int i = 0; i < animals.Count; i++)
            Console.WriteLine($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name}) | Голодний: {animals[i].IsHungry}");

        Console.Write("Оберіть номер: ");
        int idx = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("\n" + _service.FeedAnimal(animals[idx]));
    }

    private void PerformAction()
    {
        var animals = _service.GetAllAnimals();
        if (animals.Count == 0)
        {
            Console.WriteLine("\nТварин ще немає!");
            return;
        }

        Console.WriteLine("\nОберіть тварину:");
        for (int i = 0; i < animals.Count; i++)
            Console.WriteLine($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name}) | Голодний: {animals[i].IsHungry}");

        Console.Write("Оберіть номер: ");
        int idx = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("\nОберіть дію:");
        Console.WriteLine("1. Бігати");
        Console.WriteLine("2. Співати");
        Console.WriteLine("3. Літати");
        Console.WriteLine("4. Повзати");
        Console.Write("Оберіть: ");
        string actionChoice = Console.ReadLine() ?? "1";

        string action = actionChoice switch
        {
            "1" => "run",
            "2" => "sing",
            "3" => "fly",
            "4" => "crawl",
            _ => "run"
        };

        Console.WriteLine("\n" + _service.PerformAction(animals[idx], action));
    }

    private void CleanEnvironment()
    {
        Console.WriteLine("\nЯке середовище прибрати?");
        Console.WriteLine("1. Хазяїн");
        Console.WriteLine("2. Зоомагазин");
        Console.WriteLine("3. Воля (не можна прибирати)");
        Console.Write("Оберіть: ");
        int envIndex = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("\n" + _service.CleanEnvironment(envIndex));
    }

    private void MoveToWild()
    {
        var animals = _service.GetAllAnimals();
        if (animals.Count == 0)
        {
            Console.WriteLine("\nТварин ще немає!");
            return;
        }

        Console.WriteLine("\nОберіть тварину для випуску на волю:");
        for (int i = 0; i < animals.Count; i++)
            Console.WriteLine($"{i + 1}. {animals[i].Name} ({animals[i].GetType().Name})");

        Console.Write("Оберіть номер: ");
        int idx = int.Parse(Console.ReadLine() ?? "1") - 1;

        Console.WriteLine("\n" + _service.MoveAnimalToWild(animals[idx]));
    }
}