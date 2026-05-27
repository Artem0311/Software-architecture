namespace BLL;
using DAL.Entities;
using DAL.Entities.Environments;
using DAL.Factories;
using DAL.Interfaces;
using BLL.Observers;

public class AnimalService
{
    private readonly Owner _owner;
    private readonly PetShop _petShop;
    private readonly Wild _wild;
    private readonly List<IEnvironment> _environments;
    private readonly HungerNotifier _notifier;

    private readonly Dictionary<string, AnimalFactory> _factories = new()
    {
        { "cat",   new CatFactory()   },
        { "dog",   new DogFactory()   },
        { "bird",  new BirdFactory()  },
        { "snake", new SnakeFactory() },
    };

    public AnimalService()
    {
        _owner = new Owner("Іван");
        _petShop = new PetShop("Зоомагазин");
        _wild = new Wild();
        _environments = new List<IEnvironment> { _owner, _petShop, _wild };
        _notifier = new HungerNotifier(_owner.OwnerName);
    }

    public Owner GetOwner() => _owner;
    public PetShop GetPetShop() => _petShop;
    public Wild GetWild() => _wild;
    public IReadOnlyDictionary<string, AnimalFactory> GetFactories() => _factories;

    public List<Animal> GetAllAnimals()
        => _environments.SelectMany(e => e.Animals).ToList();

    public (bool success, string message, Animal? animal) CreateAnimal(string type, string name)
    {
        if (!_factories.TryGetValue(type.ToLower(), out var factory))
            return (false, $"Невідомий тип: {type}", null);

        var animal = factory.CreateAnimal(name);
        return (true, $"Тварина {name} створена!", animal);
    }

    public string AddAnimal(Animal animal, int envIndex)
    {
        try
        {
            var env = _environments[envIndex];
            env.AddAnimal(animal);
            if (env is Owner)
                SubscribeToAnimal(animal);
            return $"{animal.Name} додано!";
        }
        catch (InvalidOperationException ex)
        {
            return $"Помилка: {ex.Message}";
        }
    }

    public void SubscribeToAnimal(Animal animal)
    {
        animal.BecameHungry += _notifier.OnAnimalHungry;
        animal.Died += _notifier.OnAnimalDied;
    }

    public void UnsubscribeFromAnimal(Animal animal)
    {
        animal.BecameHungry -= _notifier.OnAnimalHungry;
        animal.Died -= _notifier.OnAnimalDied;
    }

    public string FeedAnimal(Animal animal) => animal.Feed();

    public string AdvanceTime(Animal animal, int hours) => animal.AdvanceHours(hours);

    public string PerformAction(Animal animal, string action)
    {
        if (!animal.IsAlive) return $"{animal.Name} вже не живе.";
        if (animal is not IActionable actionable) return "Тварина не вміє виконувати дії.";

        return action switch
        {
            "run"   => actionable.Run(),
            "sing"  => actionable.Sing(),
            "fly"   => actionable.Fly(),
            "crawl" => actionable.Crawl(),
            _       => "Невідома дія."
        };
    }

    public string CleanEnvironment(int envIndex)
    {
        var env = _environments[envIndex];
        if (env is ICleanableEnvironment cleanable)
            return cleanable.Clean();
        return "Це середовище не можна прибирати.";
    }

    public string MoveToWild(Animal animal)
    {
        foreach (var env in _environments)
        {
            if (!env.Animals.Contains(animal)) continue;
            env.Animals.Remove(animal);
            if (env is Owner)
                UnsubscribeFromAnimal(animal);
            _wild.AddAnimal(animal);
            return $"{animal.Name} випущено на волю!";
        }
        return $"{animal.Name} не знайдено.";
    }

    public string GetStatus()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Хазяїн ({_owner.OwnerName}): {_owner.Animals.Count} тварин. {(_owner.NeedsCleaning ? "[ПОТРЕБУЄ ПРИБИРАННЯ]" : "[ЧИСТО]")}");
        sb.AppendLine($"Зоомагазин ({_petShop.ShopName}): {_petShop.Animals.Count} тварин. {(_petShop.NeedsCleaning ? "[ПОТРЕБУЄ ПРИБИРАННЯ]" : "[ЧИСТО]")}");
        sb.AppendLine($"Воля: {_wild.Animals.Count} тварин.");
        foreach (var animal in GetAllAnimals())
            sb.AppendLine($"  - {animal.Name} ({animal.GetType().Name}) | Живий: {animal.IsAlive} | Голодний: {animal.IsHungry} | Щасливий: {animal.IsHappy}");
        return sb.ToString();
    }
}