
using Microsoft.EntityFrameworkCore;

public class AuthorService : IAuthorService
{
    private BookManagerContext _context;
    public AuthorService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(CreateAuthorDTO entity)
    {
        var author = new Author
        {
            Name = entity.Name,
            Books = new List<Book>()
        };

        await _context.SaveChangesAsync();

        return author.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<Author>().FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<DetaislAuthorDTO>> GetAllAsync()
    {
        return await _context.Set<Author>().Where(x => x.IsActive).Select(author => new DetaislAuthorDTO{
                Name = author.Name,
                Id = author.Id,
                Books = author.Books.Select(b => new DetailsAuthorBooksDTO{
                    Id = b.Id,
                    Title = b.Title
                }).ToList()
            }).ToListAsync();

    }

    public async Task<DetaislAuthorDTO> GetByIdAsync(Guid id)
    {
        return await _context.Set<Author>().Where(x => x.IsActive && x.Id == id).Select(author => new DetaislAuthorDTO{
                Name = author.Name,
                Id = author.Id,
                Books = author.Books.Select(b => new DetailsAuthorBooksDTO{
                    Id = b.Id,
                    Title = b.Title
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(CreateAuthorDTO entity, Guid id)
    {
        var existingEntity = await _context.Set<Author>().FindAsync(id);
        if (existingEntity == null || !existingEntity.IsActive) return false;

        existingEntity.Name = entity.Name;

        await _context.SaveChangesAsync();
        return true;
    }
}