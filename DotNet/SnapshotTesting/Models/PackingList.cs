namespace TestConsoleAppTemplate.Models;

public class PackingList(string name, DateTime timeOfTravel, List<PackingItem> packingItems)
{
    public string Name { get; } = name;
    public DateTime TimeOfTravel { get; } = timeOfTravel;
    public List<PackingItem> PackingItems { get; } = packingItems;

    public void AddPackingItem(PackingItem item) => PackingItems.Add(item);

    public bool RemovePackingItemByName(string itemName)
    {
        var item = PackingItems.Find(i => i.ItemName == itemName);
        if (item != null)
        {
            return PackingItems.Remove(item);
        }
        throw new ArgumentException("No Packing List or Item Found!", itemName);
    }

    public bool RemovePackingItem(PackingItem packingItem) => PackingItems.Remove(packingItem);
};
