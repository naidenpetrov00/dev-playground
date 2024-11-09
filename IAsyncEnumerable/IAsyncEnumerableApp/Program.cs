using System.Collections;

namespace IAsyncEnumerableApp;

public class Program
{
    static async Task<IEnumerable<int>> CreateListAsync()
    {
        var list = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000);
            list.Add(i);
        }
        return list;
    }

    static async IAsyncEnumerable<int> CreateAsyncList()
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000);
            yield return i;
        }
    }

    static async Task ShowListAsync()
    {
        foreach (var item in await CreateListAsync())
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {item}");
        }
    }

    static async Task ShowAsyncList()
    {
        await foreach (var item in CreateAsyncList())
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {item}");
        }
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting");
        //await ShowListAsync();
        await ShowAsyncList();
    }
}
