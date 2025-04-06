using SnapshotTesting.Enums;
using TestConsoleAppTemplate.Data;
using TestConsoleAppTemplate.Models;
using TestConsoleAppTemplate.Services;

namespace TestConsoleAppTemplate;

public class Program
{
    public static void Main()
    {
        var manPackingList = PackingLists.GetPackingListForMan()[0];
        var womenPackingList = PackingLists.GetPackingListForWomen()[0];
        var packingListService = new PackingListService();

        manPackingList.AddPackingItem(new PackingItem("Gun", 2, ItemType.Solid));
    }
}
