using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _orderService.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OrderRequest newOrder)
    {
        await _orderService.AddAsync(newOrder);
        return CreatedAtAction(nameof(GetOneById), new { id = newOrder.Id }, newOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderRequest order)
    {
        order.Id = id; // Ensure the correct ID is used for the update
        var success = await _orderService.UpdateAsync(order);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}

public class OrderRequest{
    public Guid Id {get;set;}
    public List<OrderRequestItem> Books {get;set;}
}

public class OrderRequestItem{
    public Guid BookId {get;set;}
    public int quantity {get;set;}
}