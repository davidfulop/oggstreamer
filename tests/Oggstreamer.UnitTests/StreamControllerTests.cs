using System.Net;
using NUnit.Framework;
using Oggstreamer.Controllers;

namespace Oggstreamer.UnitTests
{
    public class StreamControllerTests
    {
        StreamController _streamController;

        [SetUp]
        public void SetUp()
        {
            _streamController = new StreamController();
        }

        [Test]
        public void GetStream_returns_a_stream_with_nonzero_length()
        {
            var result = _streamController.GetStream();
            Assert.IsTrue(result.FileStream.Length > 0);
        }

        [Test]
        public void GetStream_returns_a_result_with_a_valid_ogg_filename()
        {
            var result = _streamController.GetStream();
            Assert.IsFalse(string.IsNullOrEmpty(result.FileDownloadName), "Filename is null or empty string.");
            Assert.IsTrue(result.FileDownloadName.EndsWith(".ogg"), "Filename ends with something unexpected!");
        }
        
        [Test]
        public void GetStream_returns_a_result_with_the_the_right_ContentType()
        {
            var result = _streamController.GetStream();
            Assert.AreEqual("audio/ogg", result.ContentType, "Unexpected content type.");
        }


    }
}