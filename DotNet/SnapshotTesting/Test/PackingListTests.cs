using TestConsoleAppTemplate.Data;
using TestConsoleAppTemplate.Models;
using TestConsoleAppTemplate.Services;

namespace SnapshotTesting.Test;

public class PackPackingListTestsingList
{
    private PackingListService _packingListService;
    private List<PackingList> _packingListsForMan;
    private readonly VerifySettings _verifySettings;

    public PackPackingListTestsingList()
    {
        _packingListService = new PackingListService();
        _packingListsForMan = PackingLists.GetPackingListForMan();
        _verifySettings = new VerifySettings();
        _verifySettings.UseDirectory("snapshots");
    }

    [Fact]
    public async Task GetPackingListForMan_Returns_Two_PackingLists()
    {
        var packingListForMan = PackingLists.GetPackingListForMan();

        await Verify(packingListForMan, _verifySettings);
    }

    [Fact]
    public async Task GetPackingListByName_Throws_ArgumentException_When_PackingList_NotFound()
    {
        var action = () =>
            _packingListService.GetPackingListByName("NotSuchName", _packingListsForMan);

        await Throws(action, _verifySettings);
    }
}
