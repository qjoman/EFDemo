public class DetailsBookDTO
{
    public string Title { get; set; }
    public Author Author { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public int Stock { get; set; }
    public List<string> Categories { get; set; }
    public Decimal Price { get; set; }
}