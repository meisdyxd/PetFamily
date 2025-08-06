namespace PetFamily.Application.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> AsPaginated<T>(this IQueryable<T> query, int pageSize, int page)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}