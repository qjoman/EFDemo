
using Microsoft.EntityFrameworkCore;

public class AuditLogService : IAuditLogService
{
    protected readonly BookManagerContext _context;

    public AuditLogService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLogs.ToListAsync();
    }

    public async Task<AuditLog> GetByIdAsync(Guid id)
    {
        return await _context.AuditLogs.FirstOrDefaultAsync(x => x.Id == id);
    }
}
