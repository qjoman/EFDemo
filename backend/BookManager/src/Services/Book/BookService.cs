
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
        var books = await _context.Set<Book>()
            .Include(x => x.Author)
            .Include(x => x.BookPrices)
            .Include(x => x.BookCategories)
            .Where(x => x.IsActive).ToListAsync();

        var ret = new List<DetailsBookDTO>();

        foreach (Book b in books)
        {
            var categories = await _context
                .Set<Category>()
                .Where(c => b.BookCategories.Select(x => x.CategoryId).Contains(c.Id))
                .Select(c => c.Name)
                .ToListAsync();

            ret.Add(
            new DetailsBookDTO
            {
                Author = b.Author,
                Categories = categories,
                CoverImageUrl = b.CoverImageUrl,
                Description = b.Description,
                Price = b.BookPrices.OrderByDescending(x => x.ValideFrom).FirstOrDefault().Price,
                PublishedAt = b.PublishedAt,
                Stock = b.Stock,
                Title = b.Title
            });
        }

        return ret;
    }

    public async Task<DetailsBookDTO> GetByIdAsync(Guid id)
    {
        await _context.Set<Book>()
            .Include(x => x.BookPrices)
            .Include(x => x.BookCategories)
            .Where(x => x.IsActive && x.Id == id)
            .Select(b =>
                new Book
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    Description = b.Description,
                    CoverImageUrl = b.CoverImageUrl,
                    PublishedAt = b.PublishedAt,
                    Stock = b.Stock,
                    BookPrices = b.BookPrices.OrderByDescending(p => p.Price).ToList(),
                    BookCategories = b.BookCategories.ToList()
                }
            ).FirstOrDefaultAsync();

        return new DetailsBookDTO();
    }

    public async Task<Guid> AddAsync(CreateBookDTO entity)
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
            _context.Set<Book>().AddAsync(newBook).AsTask()
        };

        foreach (var bc in entity.BookCategories)
        {
            tasks.Add(_context.Set<BookCategory>().AddAsync(new BookCategory
            {
                BookId = newBook.Id,
                CategoryId = bc
            }).AsTask());
        }

        await Task.WhenAll(tasks);

        await _context.SaveChangesAsync();

        return newBook.Id;
    }

    public async Task<bool> UpdateAsync(CreateBookDTO entity, Guid id)
    {
        var existingEntity = await _context.Set<Book>().FindAsync(id);
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
        var entity = await _context.Set<Book>().FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }
}