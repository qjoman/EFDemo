public class DetailsBookDTO
{    
    public Guid Id {get;set;}
    public string Title { get; set; }
    public DetailsAuthorBookDTO Author { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public int Stock { get; set; }
    public List<string> Categories { get; set; }
    public Decimal Price { get; set; }
    public List<DetailsBookPriceDTO> BookPrices {get;set;}
}

public class DetailsAuthorBookDTO{
    public Guid Id {get;set;}
    public string Name {get;set;}
}