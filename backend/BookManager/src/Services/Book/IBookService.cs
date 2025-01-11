public interface IBookService {
    Task<IEnumerable<DetailsBookDTO>> GetAllAsync();
    Task<DetailsBookDTO> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(CreateBookDTO entity);
    Task<bool> UpdateAsync(CreateBookDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}