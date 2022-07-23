using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace BookLibraryApi.Helpers;

public static class ImageHelper
{
    private static readonly string EnvironmentPath = @$"{Environment.CurrentDirectory}\Files\";

    public static void CreateImage(IFormFile imageFile, string name)
    {
        var image = Image.Load(imageFile.OpenReadStream());
        var path = $@"{EnvironmentPath}{name}.jpeg";
        image.Save(path, new JpegEncoder());
    }
}