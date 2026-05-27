namespace BLL;
using DAL.Entities;
using DAL.Entities.Environments;
using DAL.Interfaces;

public class AnimalService
{
    private readonly List<IEnvironment> _environments;
    private readonly Owner _owner;
    private readonly PetShop _petShop;
    private readonly Wild _wild;

    public AnimalService()
    {
        _owner = new Owner("Іван");
        _petShop = new PetShop("Зоомагазин");
        _wild = new Wild();
        _environments = new List<IEnvironment> { _owner, _petShop, _wild };
    }

    public Owner GetOwner() => _owner;
    public PetShop GetPetShop() => _petShop;
    public Wild GetWild() => _wild;

    public List<Animal> GetAllAnimals()
    {
        return _environments.SelectMany(e => e.Animals).ToList();
    }

    public string AddAnimal(Animal animal, int envIndex)
    {
        var env = _environments[envIndex];
        env.AddAnimal(animal);
        return $"{animal.Name} додано до {env.GetType().Name}!";
    }

    public string FeedAnimal(Animal animal)
    {
        return animal.Feed();
    }

    public string PerformAction(Animal animal, string action)
    {
        if (animal is not IActionable actionable)
            return $"{animal.Name} не вміє виконувати дії.";

        return action switch
        {
            "run" => actionable.Run(),
            "sing" => actionable.Sing(),
            "fly" => actionable.Fly(),
            "crawl" => actionable.Crawl(),
            _ => "Невідома дія."
        };
    }

    public string CleanEnvironment(int envIndex)
    {
        var env = _environments[envIndex];
        if (env is ICleanableEnvironment cleanable)
            return cleanable.Clean();
        
        return "Це середовище не можна прибирати.";
    }

    public string MoveAnimalToWild(Animal animal)
    {
        // знаходимо де зараз тварина
        foreach (var env in _environments)
        {
            if (env.Animals.Contains(animal))
            {
                env.Animals.Remove(animal);
                _wild.AddAnimal(animal);
                return $"{animal.Name} випущено на волю!";
            }
        }
        return $"{animal.Name} не знайдено.";
    }

    public string GetStatus()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Хазяїн ({_owner.OwnerName}): {_owner.Animals.Count} тварин. " +
                     $"{(_owner.NeedsCleaning ? "[ПОТРЕБУЄ ПРИБИРАННЯ]" : "[ЧИСТО]")}");
        
        sb.AppendLine($"Зоомагазин ({_petShop.ShopName}): {_petShop.Animals.Count} тварин. " +
                     $"{(_petShop.NeedsCleaning ? "[ПОТРЕБУЄ ПРИБИРАННЯ]" : "[ЧИСТО]")}");
        
        sb.AppendLine($"Воля: {_wild.Animals.Count} тварин. [ПРИРОДА]");

        foreach (var animal in GetAllAnimals())
        {
            sb.AppendLine($"  - {animal.Name} ({animal.GetType().Name}) | " +
                         $"Голодний: {animal.IsHungry} | " +
                         $"Щасливий: {animal.IsHappy}");
        }

        return sb.ToString();
    }
}