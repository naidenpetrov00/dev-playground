namespace TestConsoleAppTemplate.Models;

public class PackingList(string name, DateTime timeOfTravel, List<PackingItem> packingItems)
{
    public string Name { get; } = name;
    public DateTime TimeOfTravel { get; } = timeOfTravel;
    public List<PackingItem> PackingItems { get; } = packingItems;
};
