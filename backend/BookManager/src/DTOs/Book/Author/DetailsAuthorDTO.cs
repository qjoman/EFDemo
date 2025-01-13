public class DetaislAuthorDTO : CreateAuthorDTO
{
    public Guid Id { get; set; }
    public List<DetailsAuthorBooksDTO> Books {get;set;}
}

public class DetailsAuthorBooksDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}