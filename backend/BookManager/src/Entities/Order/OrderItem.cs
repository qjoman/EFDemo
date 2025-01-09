public class OrderItem : BaseModel{
    public Order Order {get; set;}
    public Book Book {get; set;}
    public int Quantity {get; set;}
    public BookPrice BookPrice {get;set;}
}