public interface IAuditLogService 
{
    Task<IEnumerable<AuditLog>> GetAllAsync();
    Task<AuditLog> GetByIdAsync(Guid id);
}
