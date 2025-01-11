using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : CrudController<User>
{
    // Add or override custom methods specific here
    public UserController(IUserService crudService) : base(crudService)
    {
    }
}