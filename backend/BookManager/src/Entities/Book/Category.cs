using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseModel{
    public string Name {set; get;}
    public string Description {get; set;}
    public virtual List<BookCategory> BookCategories {get; set;}
}