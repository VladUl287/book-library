using Common.Dtos.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Common.Dtos;

public class CreateBookModel : BookBase
{
    public IFormFile Image { get; set; }
}