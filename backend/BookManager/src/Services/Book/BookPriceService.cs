public class BookPriceService : CrudService<BookPrice>, IBookPriceService
{
    public BookPriceService(BookManagerContext context) : base(context)
    {
    }
}