

using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    protected readonly BookManagerContext _context;

    public OrderService(BookManagerContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderRequest entity)
    {
        Order order = new Order();

        order.OrderDate = DateTime.Now;
        order.OrderItems = new List<OrderItem>();
        order.OrderStatus = OrderStatus.Pending;

        List<Book> books = await _context.Books.Include(b => b.BookPrices).Where(b => b.IsActive && entity.Books.Select(x => x.BookId).Contains(b.Id)).ToListAsync();

        List<string> err = new List<string>();
        foreach (Book book in books)
        {
            var eBook = entity.Books.Where(x => x.BookId == book.Id).FirstOrDefault();
            if (book.Stock < eBook.quantity)
            {
                err.Add("Book '" + book.Title + "' has no stock (only " + book.Stock + " available)");
            }
            else
            {
                order.OrderItems.Add(new OrderItem()
                {
                    BookId = book.Id,
                    ItemPrice = book.BookPrices.OrderByDescending(x => x.ValidFrom).FirstOrDefault().Price,
                    Quantity = eBook.quantity
                });

                book.Stock -= eBook.quantity;
            }
        }
        if (err.Count > 0)
        {
            throw new Exception(string.Join("\n", err));
        }
        else
        {
            order.TotalPrice = order.OrderItems.Sum(item => item.Quantity * item.ItemPrice);
            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Order order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(x => x.Id == id && x.IsActive == true);

        if (order == null)
        {
            throw new ArgumentException("Order not found.");
        }

        order.IsActive = false;
        order.UpdatedAt = DateTime.UtcNow;

        foreach (var item in order.OrderItems)
        {
            item.IsActive = false;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.Where(x => x.IsActive).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _context.Orders.Where(x => x.IsActive && x.Id == id).FirstOrDefaultAsync();
    }

    public Task<bool> UpdateAsync(OrderRequest entity)
    {
        throw new NotImplementedException();
    }

}