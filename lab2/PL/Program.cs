using BLL;
using DAL.Entities;

var service = new AnimalService();
var animals = new List<Animal>();
bool running = true;

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (running)
{
    Console.WriteLine("\n========= СИМУЛЯТОР ТВАРИН =========");
    Console.WriteLine("1. Створити тварину");
    Console.WriteLine("2. Годувати тварину");
    Console.WriteLine("3. Виконати дію");
    Console.WriteLine("4. Симулювати час");
    Console.WriteLine("5. Прибрати середовище");
    Console.WriteLine("6. Випустити на волю");
    Console.WriteLine("7. Підписка / відписка");
    Console.WriteLine("8. Статус");
    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1":
            Console.WriteLine("Тип (cat/dog/bird/snake): ");
            var type = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Ім'я: ");
            var name = Console.ReadLine()?.Trim() ?? "";
            var (success, message, animal) = service.CreateAnimal(type, name);
            Console.WriteLine(message);
            if (!success || animal == null) break;
            Console.WriteLine("Де? 0 - Хазяїн, 1 - Зоомагазин, 2 - Воля");
            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                Console.WriteLine(service.AddAnimal(animal, idx));
                animals.Add(animal);
            }
            break;

        case "2":
            var a2 = SelectAnimal(animals);
            if (a2 != null) Console.WriteLine(service.FeedAnimal(a2));
            break;

        case "3":
            var a3 = SelectAnimal(animals);
            if (a3 == null) break;
            Console.Write("Дія (run/sing/fly/crawl): ");
            var action = Console.ReadLine()?.Trim() ?? "";
            Console.WriteLine(service.PerformAction(a3, action));
            break;

        case "4":
            var a4 = SelectAnimal(animals);
            if (a4 == null) break;
            Console.Write("Скільки годин? ");
            if (int.TryParse(Console.ReadLine(), out int hours))
                Console.WriteLine(service.AdvanceTime(a4, hours));
            break;

        case "5":
            Console.Write("0 - Хазяїн, 1 - Зоомагазин: ");
            if (int.TryParse(Console.ReadLine(), out int envIdx))
                Console.WriteLine(service.CleanEnvironment(envIdx));
            break;

        case "6":
            var a6 = SelectAnimal(animals);
            if (a6 != null) Console.WriteLine(service.MoveToWild(a6));
            break;

        case "7":
            var a7 = SelectAnimal(animals);
            if (a7 == null) break;
            Console.WriteLine("1 - Підписати, 2 - Відписати");
            var sub = Console.ReadLine()?.Trim();
            if (sub == "1") { service.SubscribeToAnimal(a7); Console.WriteLine("Підписано!"); }
            else if (sub == "2") { service.UnsubscribeFromAnimal(a7); Console.WriteLine("Відписано!"); }
            break;

        case "8":
            Console.WriteLine(service.GetStatus());
            break;

        case "0":
            running = false;
            break;
    }
}

Animal? SelectAnimal(List<Animal> list)
{
    if (list.Count == 0) { Console.WriteLine("Тварин немає."); return null; }
    Console.WriteLine("Оберіть тварину:");
    for (int i = 0; i < list.Count; i++)
        Console.WriteLine($"{i}. {list[i].Name} ({list[i].GetType().Name})");
    Console.Write("Номер: ");
    if (int.TryParse(Console.ReadLine(), out int i2) && i2 >= 0 && i2 < list.Count)
        return list[i2];
    Console.WriteLine("Невірний вибір.");
    return null;
}