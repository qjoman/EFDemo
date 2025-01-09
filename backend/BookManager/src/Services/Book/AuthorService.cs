public class AuthorService : CrudService<Author>, IAuthorService
{
    public AuthorService(BookManagerContext context) : base(context)
    {
    }
}