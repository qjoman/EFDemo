using Microsoft.EntityFrameworkCore;

[Index(nameof(Title), nameof(Author.Id), IsUnique = true)]
public class Book : BaseModel{
    public string Title {get;set;}
    public Author Author {get; set;}
    public string Description {get;set;}
    public string CoverImageUrl {get; set;}
    public string publishAt {get;set;}
    public List<BookPrice> BookPrices {get; set;}
    public int Stock {get;set;}
    public List<Category> Categories {get;set;}
}