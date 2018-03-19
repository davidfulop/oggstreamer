using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Oggstreamer.Controllers
{
    [Route("api/[controller]")]
    public class StreamController : Controller
    {
        [HttpGet]
        public FileStreamResult GetStream()
        {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes("Hello World"));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("audio/ogg"))
            {
                FileDownloadName = "test.ogg"
            };
        }
    }
}
