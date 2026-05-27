namespace DAL.Entities.Environments;
using DAL.Interfaces;
using DAL.Entities;

public class PetShop : ICleanableEnvironment
{
    public string ShopName { get; }
    public List<Animal> Animals { get; } = new();
    public bool NeedsCleaning { get; private set; } = false;

    public PetShop(string shopName)
    {
        ShopName = shopName;
    }

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
        MakeDirty();
    }

    public void MakeDirty() => NeedsCleaning = true;

    public string Clean()
    {
        if (!NeedsCleaning) return $"У {ShopName} і так чисто!";
        NeedsCleaning = false;
        foreach (var animal in Animals)
            animal.MakeHappy();
        return $"{ShopName} прибрано. Всі тварини щасливі!";
    }
}