using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : CrudController<Author>
{
    // Add or override custom methods specific here
    public AuthorController(IAuthorService crudService) : base(crudService)
    {
    }
}