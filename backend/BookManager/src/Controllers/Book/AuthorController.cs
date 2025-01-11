using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private IAuthorService _authorService;
    public AuthorController(IAuthorService authorService) 
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _authorService.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneById(Guid id)
    {
        var result = await _authorService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAuthorDTO model)
    {
        var id = await _authorService.AddAsync(model);
        return CreatedAtAction(nameof(GetOneById), new { id = id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateAuthorDTO model)
    {
        var success = await _authorService.UpdateAsync(model, id);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _authorService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}