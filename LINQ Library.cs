public class Author
{
    public int AuthorID { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }
}

public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; }
    public int AuthorID { get; set; }
    public long ISBN { get; set; }
    public int PublishedYear { get; set; }
}

//populating

public static class DataStore
{
    public static List<Author> Authors = new List<Author>
    {
        new Author { AuthorID = 1, Name = "Andre Aciman", BirthDate = new DateTime(1951, 1, 2), Country = "Egypt" },
        new Author { AuthorID = 2, Name = "William James", BirthDate = new DateTime(1842, 1, 11), Country = "United States" },
        new Author { AuthorID = 3, Name = "David Humes", BirthDate = new DateTime(1711, 5, 7), Country = "United Kingdom" },
        new Author { AuthorID = 4, Name = "Albert Camus", BirthDate = new DateTime(1913, 11, 7), Country = "Algeria" },
        new Author { AuthorID = 5, Name = "George Orwell", BirthDate = new DateTime(1903, 6, 25), Country = "India" }
    };

    public static List<Book> Books = new List<Book>
    {
        new Book { BookID = 1, Title = "Find Me", AuthorID = 1, ISBN = 9780312426781, PublishedYear = 2019 },
        new Book { BookID = 2, Title = "The Varieties of Religious Experience", AuthorID = 2, ISBN = 9780140390346, PublishedYear = 1902 },
        new Book { BookID = 3, Title = "A Treatise of Human Nature", AuthorID = 3, ISBN = 9780486432501, PublishedYear = 1739 },
        new Book { BookID = 4, Title = "The Stranger", AuthorID = 4, ISBN = 9780679720201, PublishedYear = 1942 },
        new Book { BookID = 5, Title = "Animal Farm", AuthorID = 5, ISBN = 9780451524935, PublishedYear = 1945 }
    };
}

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    [HttpGet("books/year/{year}")]
    public IActionResult GetBooksByYear(int year, [FromQuery] string order = "asc")
    {
        var books = DataStore.Books.Where(b => b.PublishedYear == year).OrderBy(b => order == "asc" ? b.PublishedYear : -b.PublishedYear).ToList();
        return Ok(books);
    }

    [HttpGet("authors/grouped/year")]
    public IActionResult GroupAuthorsByBirthYear()
    {
        var groups = DataStore.Authors.GroupBy(a => a.BirthDate.Year).Select(g => new { BirthYear = g.Key, Authors = g.ToList() });
        return Ok(groups);
    }

    [HttpGet("authors/grouped/yearCountry")]
    public IActionResult GroupAuthorsByYearAndCountry()
    {
        var groups = DataStore.Authors.GroupBy(a => new { a.BirthDate.Year, a.Country }).Select(g => new
            {g.Key.Year,g.Key.Country,Authors = g.ToList()});
        return Ok(groups);
    }

    [HttpGet("books/count")]
    public IActionResult GetBookCount()
    {
        int count = DataStore.Books.Count;
        return Ok(new { booksCount = count });
    }

    [HttpGet("books/paged")]
    public IActionResult GetBooksPaged(int pageSize, int pageNumber)
    {
        var books = DataStore.Books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return Ok(books);
    }
}
