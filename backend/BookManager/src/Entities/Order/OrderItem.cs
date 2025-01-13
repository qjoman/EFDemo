using System.Text.Json.Serialization;

public class OrderItem : BaseModel{
    public Guid OrderId {get; set;}
    [JsonIgnore]
    public virtual Order Order {get; set;}
    public Guid BookId{get;set;}
    [JsonIgnore]
    public virtual Book Book {get; set;}
    public int Quantity {get; set;}
    public decimal ItemPrice {get;set;}
}