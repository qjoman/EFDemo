public interface IBookService {
    Task<PaginatedResponse<DetailsBookDTO>> GetAllAsync(PaginationRequestDTO request);
    Task<DetailsBookDTO> GetByIdAsync(Guid id);
    Task<Book> AddAsync(CreateBookDTO entity);
    Task<Book> AddBookPriceAsync(CreateBookPriceDTO entity, Guid bookId);
    Task<bool> UpdateAsync(CreateBookDTO entity, Guid id);
    Task<bool> DeleteAsync(Guid id);
}