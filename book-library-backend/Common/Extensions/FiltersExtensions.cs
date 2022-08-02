using Domain.Filters.Abstractions;
using Domain.Filters;
using DataAccess.Models;

namespace Domain.Extensions;

public static class FiltersExtensions
{
    public static IQueryable<T> SetPageFilter<T>(this IQueryable<T> reviews, PageFilter pageFilter)
    {
        if (pageFilter.Page.HasValue && pageFilter.Size.HasValue)
        {
            var page = pageFilter.Page.Value;
            var size = pageFilter.Size.Value;
            var skip = (page - 1) * size;

            reviews = reviews.Skip(skip).Take(size);
        }

        return reviews;
    }

    public static IQueryable<Book> SetBookFilter(this IQueryable<Book> query, BookFilter bookFilter)
    {
        if (!string.IsNullOrEmpty(bookFilter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(bookFilter.Name.ToLower()));
        }
        if (bookFilter.BeginYear.HasValue)
        {
            query = query.Where(x => x.Date.Year >= bookFilter.BeginYear.Value);
        }
        if (bookFilter.EndYear.HasValue)
        {
            query = query.Where(x => x.Date.Year <= bookFilter.EndYear.Value);
        }
        if (bookFilter.Rating.HasValue)
        {
            query = query.Where(x => x.Rating >= bookFilter.Rating);
        }
        if (bookFilter.Genres.Length == 1)
        {
            query = query.Where(x => x.BooksGenres.Any(x => x.GenreId == bookFilter.Genres[0]));
        }
        else if (bookFilter.Genres.Length > 1)
        {
            query = query.Where(x => x.BooksGenres.Intersect(x.BooksGenres.Where(x => bookFilter.Genres.Contains(x.GenreId))).Any());
        }

        return query;
    }

    public static IQueryable<Review> SetReviewFilter(this IQueryable<Review> query, ReviewFilter reviewFilter)
    {
        if (reviewFilter.ViewsSort)
        {
            query = query.OrderBy(x => x.Views);
        }

        return query;
    }

    public static IQueryable<Collection> SetCollectionFilter(this IQueryable<Collection> query, CollectionFilter bookFilter)
    {

        return query;
    }
}