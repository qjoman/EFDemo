using Microsoft.EntityFrameworkCore;

public static class Seed{
    public static void Populate(ModelBuilder modelBuilder)
    {
        // DON'T use Gui.newGuid or Date.Now ---->>> https://github.com/dotnet/efcore/issues/35285
        // Seed Categories
        var categories = new List<Category>
        {
            new Category { Id = Guid.Parse("a0d2810e-5d51-4fa4-b401-7353a03e582d"), Name = "Science Fiction", Description = "Books related to futuristic concepts, space exploration, and advanced technology." },
            new Category { Id = Guid.Parse("694f18c2-de17-4e1e-9702-d146183803ed"), Name = "Fantasy", Description = "Books set in imaginary worlds with magic, mythical creatures, and heroic quests." },
            new Category { Id = Guid.Parse("768ddc89-8250-4ec1-a693-42dacebf27ab"), Name = "Thriller", Description = "Books that are intense, suspenseful, and filled with excitement." },
            new Category { Id = Guid.Parse("05b0ecd6-dc9d-4901-87db-08d23fcd940f"), Name = "Non-Fiction", Description = "Books based on real-life facts, events, and people." },
            new Category { Id = Guid.Parse("3fe16924-646b-4341-911b-0e0c1e9a6e29"), Name = "Biography", Description = "Books that tell the life story of real people." }
        };

        // Seed Authors
        var authors = new List<Author>
        {
            new Author { Id = Guid.Parse("2f6edd3c-4872-46eb-906f-394f09a42360"), Name = "John Doe" },
            new Author { Id = Guid.Parse("706da7f1-ddc2-4811-9b6b-81a2b9b76667"), Name = "Jane Smith" },
            new Author { Id = Guid.Parse("a1dd6d4a-4f98-44ab-937a-aa2156e4c728"), Name = "David Johnson" },
            new Author { Id = Guid.Parse("80cd7239-2286-4b89-b2ae-6df0a28615f2"), Name = "Emily Brown" }
        };
        
        // Seed Books
        var books = new List<Book>
        {
            new Book
            {
                Id = Guid.Parse("65b9afd2-ad36-4f69-a998-fa7e69e144d4"),
                Title = "Space Odyssey",
                AuthorId = authors[0].Id,
                Description = "A thrilling adventure across galaxies.",
                CoverImageUrl = "https://example.com/space_odyssey.jpg",
                PublishedAt = new DateTime(2020, 5, 1),
                Stock = 100
            },
            new Book
            {
                Id = Guid.Parse("47e73383-0b1d-4f95-8282-8aee18cc6370"),
                Title = "The Dragon's Call",
                AuthorId = authors[1].Id,
                Description = "A magical story of dragons and heroes.",
                CoverImageUrl = "https://example.com/dragons_call.jpg",
                PublishedAt = new DateTime(2021, 8, 15),
                Stock = 50
            },
            new Book
            {
                Id = Guid.Parse("f39d75f5-9564-453a-9fea-bb192e8422c0"),
                Title = "The Mystery of the Lost City",
                AuthorId = authors[2].Id,
                Description = "A mystery novel set in an ancient city filled with secrets.",
                CoverImageUrl = "https://example.com/lost_city.jpg",
                PublishedAt = new DateTime(2023, 8, 15),
                Stock = 70
            },
            new Book
            {
                Id = Guid.Parse("460fb0d4-289c-43d9-8055-1fbcf992dcb3"),
                Title = "Tech Revolution",
                AuthorId = authors[0].Id,
                Description = "An in-depth look at the technological innovations of the 21st century.",
                CoverImageUrl = "https://example.com/tech_revolution.jpg",
                PublishedAt = new DateTime(2024, 8, 12),
                Stock = 200
            },
            new Book
            {
                Id = Guid.Parse("ef2d97ca-f964-469c-9a45-24c757825fea"),
                Title = "Life of a Visionary",
                AuthorId = authors[3].Id,
                Description = "A biography of one of the most inspiring innovators of the century.",
                CoverImageUrl = "https://example.com/life_of_a_visionary.jpg",
                PublishedAt = new DateTime(2021, 2, 4),
                Stock = 120
            }
        };

        // Seed for book categories
        var bookCategories = new List<BookCategory>{
            new BookCategory{
                BookId = books[0].Id,
                CategoryId = categories[0].Id
            },
            new BookCategory{
                BookId = books[0].Id,
                CategoryId = categories[1].Id
            },
            new BookCategory{
                BookId = books[1].Id,
                CategoryId = categories[1].Id
            },
            new BookCategory{
                BookId = books[1].Id,
                CategoryId = categories[2].Id
            },
            new BookCategory{
                BookId = books[2].Id,
                CategoryId = categories[2].Id
            },
            new BookCategory{
                BookId = books[2].Id,
                CategoryId = categories[3].Id
            },
            new BookCategory{
                BookId = books[3].Id,
                CategoryId = categories[0].Id
            },
            new BookCategory{
                BookId = books[3].Id,
                CategoryId = categories[2].Id
            },
            new BookCategory{
                BookId = books[4].Id,
                CategoryId = categories[1].Id
            },
            new BookCategory{
                BookId = books[4].Id,
                CategoryId = categories[3].Id
            },
        };

        // Seed for book prices
        var bookPrices = new List<BookPrice>{
            new BookPrice{
                Id = Guid.Parse("4d7f7254-7c21-40d1-a1b4-4ecd34bfc01b"),
                BookId = books[0].Id,
                Price = 1.3m,
                ValidFrom = new DateTime(2020, 5, 1)
            },
            new BookPrice{
                Id = Guid.Parse("3171cc65-f0fe-49b9-acc6-09903c4f813f"),
                BookId = books[0].Id,
                Price = 13m,
                ValidFrom = new DateTime(2020, 5, 2)
            },
            new BookPrice{
                Id = Guid.Parse("5c67cacb-b117-4234-b01a-2019393ca824"),
                BookId = books[1].Id,
                Price = 20m,
                ValidFrom = new DateTime(2021, 8, 15)
            },
            new BookPrice{
                Id = Guid.Parse("404a0921-1dc5-4e59-8401-85339104b514"),
                BookId = books[1].Id,
                Price = 12m,
                ValidFrom = new DateTime(2021, 12, 25)
            },
            new BookPrice{
                Id = Guid.Parse("f4ca88d8-2bae-44d2-bd64-02ecf91a12ec"),
                BookId = books[2].Id,
                Price = 60m,
                ValidFrom = new DateTime(2023, 8, 15)
            },
            new BookPrice{
                Id = Guid.Parse("a0ad39a9-7e2b-4d66-8e1d-663477235065"),
                BookId = books[3].Id,
                Price = 62m,
                ValidFrom = new DateTime(2024, 8, 12)
            },
            new BookPrice{
                Id = Guid.Parse("3c065bad-f770-4b7e-b43a-7c3f430be3ef"),
                BookId = books[3].Id,
                Price = 65m,
                ValidFrom = new DateTime(2024, 8, 15)
            },
            new BookPrice{
                Id = Guid.Parse("15201511-cf0a-49c3-b1ed-e5d77a738884"),
                BookId = books[3].Id,
                Price = 70m,
                ValidFrom = new DateTime(2024, 8, 25)
            },
            new BookPrice{
                Id = Guid.Parse("fba213c2-199f-413d-9dd7-be44d2b90aca"),
                BookId = books[4].Id,
                Price = 62m,
                ValidFrom = new DateTime(2021, 2, 4)
            },
            new BookPrice{
                Id = Guid.Parse("850f45ee-21c8-4a3e-9eb4-745063787877"),
                BookId = books[4].Id,
                Price = 40m,
                ValidFrom = new DateTime(2022, 2, 1)
            },
            new BookPrice{
                Id = Guid.Parse("d03e0c70-f57d-47be-851e-31bd6003b419"),
                BookId = books[4].Id,
                Price = 90m,
                ValidFrom = new DateTime(2023, 1, 1)
            }
        };

        // Adding data to model
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity<BookPrice>().HasData(bookPrices);
        modelBuilder.Entity<BookCategory>().HasData(bookCategories);
    }

}