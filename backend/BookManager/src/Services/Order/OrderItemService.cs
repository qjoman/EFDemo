public class OrderItemService : CrudService<OrderItem>, IOrderItemService
{
    public OrderItemService(BookManagerContext context) : base(context)
    {
    }
}