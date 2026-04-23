using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new BookstoreDb(
            serviceProvider.GetRequiredService<DbContextOptions<BookstoreDb>>());

        if (context == null)
        {
            throw new NullReferenceException("Null BookShopDbContext");
        }

        if (context.Books.Any())
        {
            return; // db already seeded
        }

        context.Books.AddRange(
            new Book()
            {
                Title = "The Man Who Knew Too Much",
                CoverImageUrl = "https://ia601505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200013-M.jpg",
                Author = "Inwood, Stephen",
                Genre = "History",
                Publisher = "Macmillan Pub Ltd",
                PublicationYear = 2002,
                Language = "English",
                PageCount = 503,
                Price = 19.98M
            },
            new Book()
            {
                Title = "Final Flame",
                CoverImageUrl = "https://ia601505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200012-M.jpg",
                Author = "Adams, Jane",
                Genre = "Mystery",
                Publisher = "Macmillan Pub Ltd",
                PublicationYear = 1999,
                Language = "English",
                PageCount = 244,
                Price = 24.41M
            },
            new Book()
            {
                Title = "A Time To Die",
                CoverImageUrl = "https://ia601505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200001-M.jpg",
                Author = "Smith, Wilbur",
                Genre = "Adventure",
                Publisher = "Heinemann",
                PublicationYear = 1989,
                Language = "English",
                PageCount = 544,
                Price = 12M
            },
            new Book()
            {
                Title = "Shakespeare and The Loss of Eden",
                CoverImageUrl = "https://ia601505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200074-M.jpg",
                Author = "Belsey, Catherine",
                Genre = "Culture",
                Publisher = "Red Globe Press",
                PublicationYear = 1999,
                Language = "English",
                PageCount = 203,
                Price = 12.99M
            },
            new Book()
            {
                Title = "And Justice There Is None",
                CoverImageUrl = "https://ia801505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200120-M.jpg",
                Author = "Crombie, Deborah",
                Genre = "Mystery",
                Publisher = "Bantam Books",
                PublicationYear = 2002,
                Language = "English",
                PageCount = 336,
                Price = 24.69M
            },
            new Book()
            {
                Title = "The Blood-Dimmed Tide",
                CoverImageUrl = "https://ia601505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200157-M.jpg",
                Author = "Airth, Rennie",
                Genre = "Mystery",
                Publisher = "Viking",
                PublicationYear = 2004,
                Language = "English",
                PageCount = 384,
                Price = 26.07M
            },
            new Book()
            {
                Title = "Sherlock Holmes and the Running Noose",
                CoverImageUrl = "https://ia801505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200179-M.jpg",
                Author = "Thomas, Donald",
                Genre = "Mystery",
                Publisher = "Macmillan",
                PublicationYear = 2001,
                Language = "English",
                PageCount = 320,
                Price = 4.99M
            },
            new Book()
            {
                Title = "Q is for Quary",
                CoverImageUrl = "https://ia801505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200204-M.jpg",
                Author = "Grafton, Sue",
                Genre = "Mystery",
                Publisher = "Penguin",
                PublicationYear = 2002,
                Language = "English",
                PageCount = 400,
                Price = 5.99M
            },
            new Book()
            {
                Title = "On the time of Illusion",
                CoverImageUrl = "https://ia801505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200281-M.jpg",
                Author = "Ceresa, Francois",
                Genre = "History",
                Publisher = "Vintage Books",
                PublicationYear = 1975,
                Language = "English",
                PageCount = 416,
                Price = 22.59M
            },
            new Book()
            {
                Title = "Collaborator",
                CoverImageUrl = "https://ia801505.us.archive.org/view_archive.php?archive=/11/items/m_covers_0000/m_covers_0000_20.tar&file=0000200274-M.jpg",
                Author = "Davies, Murray",
                Genre = "Historical Fiction",
                Publisher = "Macmillan",
                PublicationYear = 2004,
                Language = "English",
                PageCount = 647,
                Price = 14.56M
            });

        context.SaveChanges();
    }
}