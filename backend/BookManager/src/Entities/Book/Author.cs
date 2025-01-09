using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Author : BaseModel
{
    public required string Name { get; set; }
    public List<Book> books { get; set; }
}