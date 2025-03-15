namespace Modello.Infrastructure.Data.Extensions;

public static class EfExtensions
{
    public static async Task<PagedList<TValue>> ToPagedListAsync<TValue>(this IQueryable<TValue> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default) where TValue : class
    {
        var totalItems = await queryable.CountAsync(cancellationToken);

        var items = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedList<TValue>(pageNumber, pageSize, totalItems, items);
    }
}
