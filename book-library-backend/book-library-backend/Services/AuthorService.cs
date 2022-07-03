using AutoMapper;
using Common.Dtos;
using DataAccess;
using DataAccess.Models;
using BookLibraryApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Common.Errors;
using Common.Filters;

namespace BookLibraryApi.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper mapper;
    private readonly DatabaseContext dbContext;

    public AuthorService(DatabaseContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<OneOf<AuthorModel, Error>> Create(AuthorModel authorModel)
    {
        var exists = await dbContext.Authors.AnyAsync(e => e.Name == authorModel.Name);

        if (exists)
        {
            return Errors.LoginFaild;
        }

        var author = mapper.Map<Author>(authorModel);

        await dbContext.Authors.AddAsync(author);
        await dbContext.SaveChangesAsync();

        return authorModel;
    }

    public async Task<IEnumerable<AuthorModel>> GetAll()
    {
        var authors = await dbContext.Authors.ToListAsync();

        return mapper.Map<IEnumerable<AuthorModel>>(authors);
    }

    public async Task Remove(AuthorModel model)
    {
        var author = mapper.Map<Author>(model);

        dbContext.Authors.Remove(author);
        await dbContext.SaveChangesAsync();
    }

    public async Task<AuthorModel> Update(AuthorModel authorModel)
    {
        var author = mapper.Map<Author>(authorModel);

        dbContext.Authors.Update(author);
        await dbContext.SaveChangesAsync();

        return authorModel;
    }
}