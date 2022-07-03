using Microsoft.AspNetCore.Http;

namespace Common.Dtos
{
    public class CreateBookModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public int PagesCount { get; set; }
        public AuthorModel[] AuthorModels { get; set; } = Array.Empty<AuthorModel>();
    }
}
