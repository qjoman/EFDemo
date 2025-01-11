using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(BookId), nameof(CategoryId))]
public class BookCategory{
    public Guid BookId {get;set;}
    public Guid CategoryId {get;set;}
}