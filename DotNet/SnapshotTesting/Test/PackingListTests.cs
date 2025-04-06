using TestConsoleAppTemplate.Data;

namespace SnapshotTesting.Test;

public class PackPackingListTestsingList
{
    [Fact]
    public async Task GetPackingListForMan_Returns_Two_PackingLists()
    {
        var packingListForMan = PackingLists.GetPackingListForMan();

        await Verify(packingListForMan);
    }
}
