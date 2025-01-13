public interface IBookService {
    Task<IEnumerable<DetailsBookDTO>> GetAllAsync();
    Task<DetailsBookDTO> GetByIdAsync(Guid id);
    Task<Book> AddAsync(CreateBookDTO entity);
    Task<Book> AddBookPriceAsync(CreateBookPriceDTO entity, Guid bookId);
    Task<bool> UpdateAsync(CreateBookDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}