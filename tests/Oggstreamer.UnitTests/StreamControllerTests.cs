using System.Net;
using NUnit.Framework;
using Oggstreamer.Controllers;
using NSubstitute;
using Oggstreamer.Providers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oggstreamer.UnitTests
{
    public class StreamControllerTests
    {
        private const int MIN_AUDIOQUALITY = 0;
        private const int MAX_AUDIOQUALITY = 5;
        private const int INVALID_AUDIOQUALITY = 99;
        StreamController _streamController;

        [SetUp]
        public void SetUp()
        {
            var mediaStreamProvider = Substitute.For<IMediaStreamProvider>();
            mediaStreamProvider.GetMediaStream(Arg.Is<int>(i => i >= 0 && i <= 5))
                .Returns(new MemoryStream(Encoding.ASCII.GetBytes("This is a test content.")));
            _streamController = new StreamController(mediaStreamProvider);
        }

        [Test]
        public async Task GetStream_returns_a_stream_with_nonzero_length()
        {
            var result = await _streamController.GetStream(MIN_AUDIOQUALITY) as FileStreamResult;
            Assert.IsTrue(result.FileStream.Length > 0);
        }

        [Test]
        public async Task GetStream_returns_a_result_with_a_valid_ogg_filename()
        {
            var result = await _streamController.GetStream(MIN_AUDIOQUALITY) as FileStreamResult;
            Assert.IsFalse(string.IsNullOrEmpty(result.FileDownloadName), "Filename is null or empty string.");
            Assert.IsTrue(result.FileDownloadName.EndsWith(".ogg"), "Filename ends with something unexpected!");
        }
        
        [Test]
        public async Task GetStream_returns_a_result_with_the_the_right_ContentType()
        {
            var result = await _streamController.GetStream(MIN_AUDIOQUALITY) as FileStreamResult;
            Assert.AreEqual("audio/ogg", result.ContentType, "Unexpected content type.");
        }
        
        [Test]
        public async Task GetStream_returns_400BadRequest_for_out_of_bounds_audioQuality()
        {
            var result = await _streamController.GetStream(INVALID_AUDIOQUALITY) as StatusCodeResult;

            Assert.IsNotNull(result, "Result was not a StatusCodeResult.");
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode, $"Unexpected {result.StatusCode} status.");
        }
    }
}