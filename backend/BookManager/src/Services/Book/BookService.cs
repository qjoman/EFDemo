
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private BookManagerContext _context;
    public BookService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DetailsBookDTO>> GetAllAsync()
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
                Author = new DetailsAuthorBookDTO{
                    Id = b.Author.Id,
                    Name = b.Author.Name
                },
                Categories = b.BookCategories.Select(c => c.Category.Name).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description,
                Price = b.BookPrices.OrderByDescending(x => x.ValideFrom).FirstOrDefault().Price,
                BookPrices = b.BookPrices.OrderByDescending(x => x.ValideFrom).Select(bc => new DetailsBookPriceDTO{
                    Id = bc.Id,
                    Price = bc.Price,
                    ValideFrom = bc.ValideFrom
                }).ToList(),
                PublishedAt = b.PublishedAt,
                Stock = b.Stock,
                Title = b.Title
            }).ToListAsync();
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
                Author = new DetailsAuthorBookDTO{
                    Id = b.Author.Id,
                    Name = b.Author.Name
                },
                Categories = b.BookCategories.Select(c => c.Category.Name).ToList(),
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description,
                Price = b.BookPrices.OrderByDescending(x => x.ValideFrom).FirstOrDefault().Price,
                BookPrices = b.BookPrices.OrderByDescending(x => x.ValideFrom).Select(bc => new DetailsBookPriceDTO{
                    Id = bc.Id,
                    Price = bc.Price,
                    ValideFrom = bc.ValideFrom
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
            BookPrices = new List<BookPrice>()
        };

        foreach (var price in entity.BookPrices)
        {
            newBook.BookPrices.Add(new BookPrice
            {
                Price = price.Price,
                ValideFrom = price.ValideFrom
            });
        }

        var tasks = new List<Task>
        {
            _context.Books.AddAsync(newBook).AsTask()
        };

        foreach (var bc in entity.BookCategories)
        {
            tasks.Add(_context.BookCategories.AddAsync(new BookCategory
            {
                BookId = newBook.Id,
                CategoryId = bc
            }).AsTask());
        }

        await Task.WhenAll(tasks);

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
        existingEntity.BookPrices = new List<BookPrice>();

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
            ValideFrom = entity.ValideFrom
        };

        var book = await _context.Books.Include(x => x.BookPrices).Where(x => x.Id == bookId).FirstOrDefaultAsync();
        book.BookPrices.Add(bookPrice);

        await _context.SaveChangesAsync();

        return book;
    }
}