public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(Guid id);
    Task AddAsync(OrderRequest entity);
    Task<bool> UpdateAsync(OrderRequest entity);
    Task<bool> DeleteAsync(Guid id);
}