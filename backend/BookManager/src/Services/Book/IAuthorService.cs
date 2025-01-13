public interface IAuthorService
{
    Task<IEnumerable<DetaislAuthorDTO>> GetAllAsync();
    Task<DetaislAuthorDTO> GetByIdAsync(Guid id);
    Task<Author> AddAsync(CreateAuthorDTO entity);
    Task<bool> UpdateAsync(CreateAuthorDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}