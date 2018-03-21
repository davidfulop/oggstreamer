using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Oggstreamer.Providers;
using System.Threading.Tasks;

namespace Oggstreamer.Controllers
{
    [Route("api/[controller]")]
    public class StreamController : Controller
    {
        private readonly IMediaStreamProvider _mediaStreamProvider;

        public StreamController(IMediaStreamProvider mediaStreamProvider)
        {
            _mediaStreamProvider = mediaStreamProvider ?? throw new System.ArgumentNullException(nameof(mediaStreamProvider));
        }

        [HttpGet("{audioQuality}")]
        public async Task<ActionResult> GetStream(int audioQuality)
        {
            if (audioQuality < 0 || audioQuality > 5)
                return StatusCode(400);
            var stream = await _mediaStreamProvider.GetMediaStream(audioQuality);
            return new FileStreamResult(stream, new MediaTypeHeaderValue("audio/ogg"))
            {
                FileDownloadName = $"testfile_q{audioQuality}.ogg"
            };
        }
    }
}
