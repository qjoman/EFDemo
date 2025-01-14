using System.Text.Json.Serialization;

public class BookPrice: BaseModel{
    public Guid BookId{set;get;}
    [JsonIgnore]
    public virtual Book Book {get; set;}
    public DateTime ValidFrom {get;set;}
    public decimal Price {get;set;}
}