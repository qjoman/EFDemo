using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Title), nameof(Author.Id), IsUnique = true)]
public class Book : BaseModel
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    [JsonIgnore]
    public virtual Author Author { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public int Stock { get; set; }
    public virtual List<BookCategory> BookCategories { get; set; }
    public virtual List<BookPrice> BookPrices {get;set;}
}