public class OrderService : CrudService<Order>, IOrderService
{
    public OrderService(BookManagerContext context) : base(context)
    {
    }
}