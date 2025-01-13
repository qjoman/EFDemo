
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private BookManagerContext _context;
    public BookService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResponse<DetailsBookDTO>> GetAllAsync(PaginationRequestDTO request)
    {
        return await _context.Books
            .Include(x => x.Author)
            .Include(x => x.BookPrices)
            .Include(x => x.BookCategories)
            .ThenInclude(x => x.Category)
            .Where(x => x.IsActive)
            .Select(b => new DetailsBookDTO
            {
                Id = b.Id,
                Author = new DetailsAuthorBookDTO
                {
                    Id = b.Author.Id,
                    Name = b.Author.Name
                },
                Categories = b.BookCategories.Select(c => c.Category.Name).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description,
                Price = b.BookPrices.OrderByDescending(x => x.ValidFrom).FirstOrDefault().Price,
                BookPrices = b.BookPrices.OrderByDescending(x => x.ValidFrom).Select(bc => new DetailsBookPriceDTO
                {
                    Id = bc.Id,
                    Price = bc.Price,
                    ValideFrom = bc.ValidFrom
                }).ToList(),
                PublishedAt = b.PublishedAt,
                Stock = b.Stock,
                Title = b.Title
            }).ToPaginatedResponseAsync(request);
    }

    public async Task<DetailsBookDTO> GetByIdAsync(Guid id)
    {
        return await _context.Books
            .Include(x => x.Author)
            .Include(x => x.BookPrices)
            .Include(x => x.BookCategories)
            .ThenInclude(x => x.Category)
            .Where(x => x.Id == id && x.IsActive)
            .Select(b => new DetailsBookDTO
            {
                Id = b.Id,
                Author = new DetailsAuthorBookDTO
                {
                    Id = b.Author.Id,
                    Name = b.Author.Name
                },
                Categories = b.BookCategories.Select(c => c.Category.Name).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description,
                Price = b.BookPrices.OrderByDescending(x => x.ValidFrom).FirstOrDefault().Price,
                BookPrices = b.BookPrices.OrderByDescending(x => x.ValidFrom).Select(bc => new DetailsBookPriceDTO
                {
                    Id = bc.Id,
                    Price = bc.Price,
                    ValideFrom = bc.ValidFrom
                }).ToList(),
                PublishedAt = b.PublishedAt,
                Stock = b.Stock,
                Title = b.Title
            }).FirstOrDefaultAsync();
    }

    public async Task<Book> AddAsync(CreateBookDTO entity)
    {
        var newBook = new Book
        {
            AuthorId = entity.AuthorId,
            CoverImageUrl = entity.CoverImageUrl,
            Description = entity.Description,
            PublishedAt = entity.PublishedAt,
            Stock = entity.Stock,
            Title = entity.Title,
            BookPrices = entity.BookPrices.Select(price => 
                new BookPrice{
                    Price = price.Price,
                    ValidFrom = price.ValideFrom
                }
            ).ToList(),
            BookCategories = entity.BookCategories.Select(cat => 
                new BookCategory{
                    BookId = Guid.Empty,
                    CategoryId = cat
                }
            ).ToList()
        };

        await _context.Books.AddAsync(newBook);
        await _context.SaveChangesAsync();

        return newBook;
    }

    public async Task<bool> UpdateAsync(CreateBookDTO entity, Guid id)
    {
        var existingEntity = await _context.Books.FindAsync(id);
        if (existingEntity == null || !existingEntity.IsActive) return false;

        existingEntity.AuthorId = entity.AuthorId;
        existingEntity.CoverImageUrl = entity.CoverImageUrl;
        existingEntity.Description = entity.Description;
        existingEntity.PublishedAt = entity.PublishedAt;
        existingEntity.Stock = entity.Stock;
        existingEntity.Title = entity.Title;
        existingEntity.BookPrices = entity.BookPrices.Select(price => new BookPrice
        {
            Price = price.Price,
            ValidFrom = price.ValideFrom
        }).ToList();
        existingEntity.BookCategories = entity.BookCategories.Select(cat => 
            new BookCategory{
                BookId = Guid.Empty,
                CategoryId = cat
            }
        ).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Books.FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Book> AddBookPriceAsync(CreateBookPriceDTO entity, Guid bookId)
    {
        var bookPrice = new BookPrice
        {
            BookId = bookId,
            Price = entity.Price,
            ValidFrom = entity.ValideFrom
        };

        var book = await _context.Books.Include(x => x.BookPrices).Where(x => x.Id == bookId).FirstOrDefaultAsync();
        book.BookPrices.Add(bookPrice);

        await _context.SaveChangesAsync();

        return book;
    }
}