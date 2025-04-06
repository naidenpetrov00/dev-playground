using TestConsoleAppTemplate.Models;

namespace TestConsoleAppTemplate.Services;

public class PackingListService
{
    public PackingList GetPackingListByName(string packingListName, List<PackingList> packingLists)
    {
        var packingList = packingLists.Find(pL => pL.Name == packingListName);
        if (packingList != null)
            return packingList;

        throw new ArgumentException("No Packing List Found!", packingListName);
    }

    public void AddPackingList(PackingList packingList, List<PackingList> packingLists)
    {
        packingLists.Add(packingList);
    }

    public bool RemovePackingListByName(string packingListName, List<PackingList> packingLists)
    {
        var packingList = packingLists.Find(pL => pL.Name == packingListName);
        if (packingList != null)
            packingLists.Remove(packingList);

        throw new ArgumentException("No Packing List Found!", packingListName);
    }
}
