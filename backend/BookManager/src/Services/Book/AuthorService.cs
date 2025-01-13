
using Microsoft.EntityFrameworkCore;

public class AuthorService : IAuthorService
{
    private BookManagerContext _context;
    public AuthorService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<Author> AddAsync(CreateAuthorDTO entity)
    {
        var author = new Author
        {
            Name = entity.Name,
            Books = new List<Book>()
        };

        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Authors.FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PaginatedResponse<DetaislAuthorDTO>> GetAllAsync(PaginationRequestDTO request)
    {
        return await _context.Authors
            .Where(x => x.IsActive)
            .Select(author => new DetaislAuthorDTO
            {
                Name = author.Name,
                Id = author.Id,
                Books = author.Books.Select(b => new DetailsAuthorBooksDTO
                {
                    Id = b.Id,
                    Title = b.Title
                }).ToList()
            }).ToPaginatedResponseAsync(request);
    }

    public async Task<DetaislAuthorDTO> GetByIdAsync(Guid id)
    {
        return await _context.Authors.Where(x => x.IsActive && x.Id == id).Select(author => new DetaislAuthorDTO
        {
            Name = author.Name,
            Id = author.Id,
            Books = author.Books.Select(b => new DetailsAuthorBooksDTO
            {
                Id = b.Id,
                Title = b.Title
            }).ToList()
        }).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(CreateAuthorDTO entity, Guid id)
    {
        var existingEntity = await _context.Authors.FindAsync(id);
        if (existingEntity == null || !existingEntity.IsActive) return false;

        existingEntity.Name = entity.Name;

        await _context.SaveChangesAsync();
        return true;
    }
}