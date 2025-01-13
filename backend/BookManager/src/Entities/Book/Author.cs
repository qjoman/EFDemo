using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Author : BaseModel
{
    public string Name { get; set; }
    public List<Guid> BooksIds {set; get;}
    [JsonIgnore]
    public virtual List<Book> Books { get; set; }
}
