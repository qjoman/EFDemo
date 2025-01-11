using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class CrudService<T> : IBaseCrudService<T> where T : BaseModel
{
    protected readonly BookManagerContext _context;

    public CrudService(BookManagerContext context)
    {
        _context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(x => x.IsActive).ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
        if (existingEntity == null || !existingEntity.IsActive) return false;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            if (!property.CanWrite)
                continue;

            var newValue = property.GetValue(entity);

            property.SetValue(existingEntity, newValue);
        }

        existingEntity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null || !entity.IsActive) return false;

        entity.IsActive = false;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}
