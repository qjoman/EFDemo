public interface ICategoryService
{
    Task<IEnumerable<DetailsCategoryDTO>> GetAllAsync();
    Task<DetailsCategoryDTO> GetByIdAsync(Guid id);
    Task<Category> AddAsync(CreateCategoryDTO entity);
    Task<bool> UpdateAsync(CreateCategoryDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}