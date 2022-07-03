using Microsoft.AspNetCore.Http;

namespace Common.Dtos
{
    public sealed class BookModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = String.Empty;
        public int PagesCount { get; set; }
        public AuthorModel[] AuthorModels { get; set; } = Array.Empty<AuthorModel>();
    }
}