public class BookPrice: BaseModel{
    public Book Book {get; set;}
    public DateTime valideFrom {get;set;}
    public decimal Price {get;set;}
}