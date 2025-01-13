public interface IAuthorService
{
    Task<PaginatedResponse<DetaislAuthorDTO>> GetAllAsync(PaginationRequestDTO request);
    Task<DetaislAuthorDTO> GetByIdAsync(Guid id);
    Task<Author> AddAsync(CreateAuthorDTO entity);
    Task<bool> UpdateAsync(CreateAuthorDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}