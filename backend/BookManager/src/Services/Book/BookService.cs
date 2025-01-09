public class BookService : CrudService<Book>, IBookService
{
    public BookService(BookManagerContext context) : base(context)
    {
    }
}