using System.Text.Json.Serialization;

public class Order: BaseModel{
    public User Customer {get; set;}
    public DateTime OrderDate {get; set;}
    public Decimal TotalPrice {get; set;}
    public OrderStatus OrderStatus {get; set;}
    public List<Guid> OrderItemsId {get;set;}
    [JsonIgnore]
    public virtual List<OrderItem> OrderItems {get;set;}
}

public enum OrderStatus{
    Pending,
    Shipped,
    Finished
}