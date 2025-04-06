using TestConsoleAppTemplate.Enums;
using TestConsoleAppTemplate.Models;

namespace TestConsoleAppTemplate.Data;

public static class PackingLists
{
    public static List<PackingList> GetPackingListForWomen() =>
        [
            new(
                "Beach Trip",
                new DateTime(2025, 6, 15),
                [
                    new("Swimsuit", 2, ItemType.Soft),
                    new("Sunscreen", 1, ItemType.Liquid),
                    new("Towel", 3, ItemType.Soft),
                    new("Dress", 2, ItemType.Soft),
                ]
            ),
            new(
                "Shopping Trip",
                new DateTime(2025, 4, 10),
                [
                    new("Handbag", 2, ItemType.Solid),
                    new("Credit Card", 1, ItemType.Solid),
                    new("Sunglasses", 1, ItemType.Solid),
                ]
            ),
        ];

    public static List<PackingList> GetPackingListForMan() =>
        [
            new(
                "Business Trip",
                new DateTime(2025, 3, 25),
                [
                    new("Laptop", 5, ItemType.Solid),
                    new("Formal Shoes", 4, ItemType.Soft),
                    new("Notebook", 2, ItemType.Solid),
                    new("Tie", 1, ItemType.Soft),
                ]
            ),
            new(
                "Hiking Adventure",
                new DateTime(2025, 7, 10),
                [
                    new("Backpack", 5, ItemType.Solid),
                    new("Water Bottle", 2, ItemType.Liquid),
                    new("Tent", 10, ItemType.Soft),
                    new("Hiking Boots", 4, ItemType.Soft),
                ]
            ),
        ];
}
