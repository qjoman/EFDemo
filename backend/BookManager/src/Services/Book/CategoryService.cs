public class CategoryService : CrudService<Category>, ICategoryService
{
    public CategoryService(BookManagerContext context) : base(context)
    {
    }
}