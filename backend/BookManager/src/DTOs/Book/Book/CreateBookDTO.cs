public class CreateBookDTO()
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public int Stock { get; set; }
    public List<Guid> BookCategories { get; set; }
    public List<BookPriceDTO> BookPrices { get; set; }
}