using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequestDTO request)
    {
        var results = await _bookService.GetAllAsync(request);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneById(Guid id)
    {
        var result = await _bookService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookDTO model)
    {
        var entity = await _bookService.AddAsync(model);
        return CreatedAtAction(nameof(GetOneById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateBookDTO model)
    {
        var success = await _bookService.UpdateAsync(model, id);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _bookService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/BookPrices")]
    public async Task<IActionResult> AddBookPrice(Guid id, [FromBody] CreateBookPriceDTO model){
         var entity = await _bookService.AddBookPriceAsync(model,id);
         
        return CreatedAtAction(nameof(GetOneById), new { id = entity.Id }, entity); 
    }
}
