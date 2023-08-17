using Taav.Contracts.Interfaces;

namespace ManageShop.Infrastucture.Queries;

public static class QueryExtensions
{
    public static async Task<IPageResult<T>> Paginate<T>(
        this IQueryable<T> query,
        IPagination pagination)
        where T : class
    {
        if (pagination is { Limit: { }, Offset: { } })
            return await query.Page(pagination);

        return await query.Page();
    }

    public static IQueryable<T> Sort<T>(
        this IQueryable<T> query,
        ISort expression)
        where T : new()
    {
        return expression is { } ? query.SortQuery(expression) : query;
    }
}