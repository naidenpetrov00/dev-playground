using TestConsoleAppTemplate.Data;
using TestConsoleAppTemplate.Services;

namespace TestConsoleAppTemplate;

public class Program
{
    public static void Main()
    {
        var manPackingList = PackingLists.GetPackingListForMan();
        var womenPackingList = PackingLists.GetPackingListForWomen();
        var packingListService = new PackingListService();
    }
}
