
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private BookManagerContext _context;
    public CategoryService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<Category> AddAsync(CreateCategoryDTO entity)
    {
        var category = new Category{
            Name = entity.Name,
            Description = entity.Description
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<DetailsCategoryDTO>> GetAllAsync()
    {
        return await _context.Categories
                .Include(x => x.BookCategories)
                .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(c => new DetailsCategoryDTO{
                    Name = c.Name,
                    Description = c.Description,
                    Id = c.Id,
                    Books = c.BookCategories.Select(bc => new DetailsBooksCategoryDTO{
                        Id = bc.Book.Id,
                        Title = bc.Book.Title
                    }).ToList()
                }).ToListAsync();
    }

    public async Task<DetailsCategoryDTO> GetByIdAsync(Guid id)
    {
        return await _context.Categories
                .Include(x => x.BookCategories)
                .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true && x.Id == id)
                .Select(c => new DetailsCategoryDTO{
                    Name = c.Name,
                    Description = c.Description,
                    Id = c.Id,
                    Books = c.BookCategories.Select(bc => new DetailsBooksCategoryDTO{
                        Id = bc.Book.Id,
                        Title = bc.Book.Title
                    }).ToList()
                }).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(CreateCategoryDTO entity, Guid id)
    {
        var category = await _context.Categories.Where(x => x.Id == id && x.IsActive).FirstOrDefaultAsync();
        
        category.Description = entity.Description;
        category.Name = entity.Name;
        category.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }
}