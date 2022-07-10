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


        [HttpGet("{id:Guid}")]
        public IActionResult GetPicture(Guid id)
        {
            var path = $@"{Directory}\{id}.jpg";

            return PhysicalFile(path, "image/jpeg");
        }
    }
}