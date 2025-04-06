using SnapshotTesting.Enums;

namespace TestConsoleAppTemplate.Models;

public class PackingItem(string itemName, int itemSize, ItemType itemType)
{
    public string ItemName { get; } = itemName;
    public int ItemSize { get; } = itemSize;
    public ItemType ItemType { get; } = itemType;
};
