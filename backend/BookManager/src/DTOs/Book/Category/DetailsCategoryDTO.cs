public class DetailsCategoryDTO : CreateCategoryDTO{
    public Guid Id {get;set;}
    public List<DetailsBooksCategoryDTO> Books {get;set;}
}

public class DetailsBooksCategoryDTO{
    public Guid Id {get;set;}
    public string Title {get;set;}
}