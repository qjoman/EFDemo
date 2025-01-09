using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseModel{
    public string Name {set; get;}
    public string Description {get; set;}
    public List<Book> Books {get; set;}
}