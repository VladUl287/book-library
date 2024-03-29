﻿using Domain.Dtos;

namespace BookLibraryApi.Services.Contracts;

public interface IBookmarkService
{
    Task<IEnumerable<BookView>> Get(Guid userId);
    Task Add(Guid userId, Guid bookId);
    Task Remove(Guid userId, Guid bookId);
}