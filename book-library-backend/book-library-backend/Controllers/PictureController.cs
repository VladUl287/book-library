using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PictureController : ControllerBase
    {
        private readonly string Directory = $@"{Environment.CurrentDirectory}\Files";

        public PictureController()
        {
            
        }


        [HttpGet("{name}")]
        public IActionResult GetPicture(string name)
        {
            var path = $@"{Directory}\{name}.jpg";

            return PhysicalFile(path, "image/jpeg");
        }
    }
}