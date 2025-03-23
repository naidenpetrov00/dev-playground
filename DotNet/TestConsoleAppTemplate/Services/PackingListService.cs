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

    public void AddPackingItem(
        string packingListName,
        PackingItem item,
        List<PackingList> packingLists
    )
    {
        var packingList = packingLists.Find(pL => pL.Name == packingListName);
        if (packingList != null)
        {
            packingList.PackingItems.Add(item);
            return;
        }
        throw new ArgumentException("No Packing List Found!", packingListName);
    }

    public bool RemovePackingItem(
        string packingListName,
        string itemName,
        List<PackingList> packingLists
    )
    {
        var packingList = packingLists.Find(pL => pL.Name == packingListName);
        if (packingList != null)
        {
            var item = packingList.PackingItems.Find(i => i.ItemName == itemName);
            if (item != null)
            {
                return packingList.PackingItems.Remove(item);
            }
        }
        throw new ArgumentException("No Packing List or Item Found!", packingListName);
    }
}
