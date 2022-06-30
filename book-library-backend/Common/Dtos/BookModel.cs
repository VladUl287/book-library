using Microsoft.AspNetCore.Http;

namespace Common.Dtos
{
    public sealed class BookModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public int PagesCount { get; set; }
    }
}