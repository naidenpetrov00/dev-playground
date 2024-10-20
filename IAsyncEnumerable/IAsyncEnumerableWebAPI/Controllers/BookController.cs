using IAsyncEnumerableWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IAsyncEnumerableWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly List<Book> books;
    private readonly ILogger logger;

    public BookController(ILogger<BookController> logger)
    {
        this.books = new List<Book>
        {
            new("Dr No", "Thriller", 4),
            new Book("Goldfinger", "Thriller", 5),
            new Book("Farenheit 451", "SF", 4),
            new Book("2001 - A Space Odyssey", "SF", 5),
            new Book("Frankenstein", "Horror", 5),
            new Book("Twelve", "Horror", 5),
            new Book("Varney the Vampire", "Horror", 3),
            new Book("Emma", "Classic", 4),
        };
        this.logger = logger;
    }

    public IEnumerable<Book> Books => books;

    [HttpGet("Recommendations/{genre}")]
    public IEnumerable<Book> Recommendations(string genre)
    {
        logger.LogInformation($"Request for {genre}");
        Thread.Sleep(5000);

        var genreBooks = books.Where(b => string.Compare(b.Genre, genre, true) == 0);

        var maxRating = genreBooks.Max(b => b.Rating);

        return genreBooks.Where(b => b.Rating == maxRating);
    }
}
