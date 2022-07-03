using Common.Filters.Abstractions;

namespace BookLibraryApi.Helpers;

public static class FiltersHelper
{
    public static IQueryable<T> SetPageFilter<T>(PageFilter pageFilter, IQueryable<T> reviews)
    {
        if (pageFilter.Page.HasValue && pageFilter.Size.HasValue)
        {
            var page = pageFilter.Page.Value;
            var size = pageFilter.Size.Value;
            var skip = (page - 1) * size;

            reviews = reviews.Skip(skip).Take(page);
        }

        return reviews;
    }
}