public class DetaislAuthorDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DetailsAuthorBooksDTO> Books {get;set;}
}

public class DetailsAuthorBooksDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}