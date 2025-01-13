using Microsoft.AspNetCore.Mvc;

public class CrudController<T> : ControllerBase where T : BaseModel
{
    private readonly IBaseCrudService<T> _crudService;

    public CrudController(IBaseCrudService<T> crudService)
    {
        _crudService = crudService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _crudService.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneById(Guid id)
    {
        var result = await _crudService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] T model)
    {
        await _crudService.AddAsync(model);
        return CreatedAtAction(nameof(GetOneById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] T model)
    {
        model.Id = id; // Ensure the correct ID is used for the update
        var success = await _crudService.UpdateAsync(model);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _crudService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}