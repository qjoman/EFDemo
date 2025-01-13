using Microsoft.EntityFrameworkCore;

public static class QueryableExtensions
{
    public static async Task<PaginatedResponse<T>> ToPaginatedResponseAsync<T>(
        this IQueryable<T> query,
        PaginationRequestDTO request)
    {
        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginatedResponse<T>
        {
            CurrentPage = request.PageNumber,
            Items = items,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}