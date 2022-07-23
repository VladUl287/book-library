using Domain.Dtos.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class BookCreate : BookBase
{
    public IFormFile ImageFile { get; set; }
}