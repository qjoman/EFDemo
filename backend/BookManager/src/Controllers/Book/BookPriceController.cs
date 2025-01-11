using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookPriceController : CrudController<BookPrice>
{
    // Add or override custom methods specific here
    public BookPriceController(IBookPriceService crudService) : base(crudService)
    {
    }
}