using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : CrudController<Category>
{
    // Add or override custom methods specific here
    public CategoryController(ICategoryService crudService) : base(crudService)
    {
    }
}