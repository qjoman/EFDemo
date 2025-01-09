public class Order: BaseModel{
    public User User {get; set;}
    public DateTime OrderDate {get; set;}
    public Decimal TotalPrice {get; set;}
    public OrderStatus OrderStatus {get; set;}
    public List<OrderItem> OrderItems {get;set;}
}

public enum OrderStatus{
    Pending,
    Shipped,
    Finished
}