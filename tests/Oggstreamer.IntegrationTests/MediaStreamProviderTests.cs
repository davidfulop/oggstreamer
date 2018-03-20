using NUnit.Framework;
using NSubstitute;
using Oggstreamer.Providers;
using System.Threading.Tasks;

namespace Oggstreamer.IntegrationTests
{
    public class MediaStreamProviderTests
    {
        private MediaStreamProvider _mediaStreamProvider;

        [SetUp]
        public void SetUp()
        {
            var fakeTranscodingProvider = Substitute.For<ITranscodingProvider>();
            fakeTranscodingProvider.Transcode(Arg.Any<string>(), Arg.Any<string>()).Returns(
                Task<string>.Factory.StartNew(() => {
                    Task.Delay(1000).Wait();
                    return "0";
                }));
            _mediaStreamProvider = new MediaStreamProvider(fakeTranscodingProvider);
        }

        [Test]
        public async Task GetMediaStream_returns_a_stream()
        {
            var result = await _mediaStreamProvider.GetMediaStream();
            Assert.IsNotNull(result, "MediaStreamProvider.GetMediaStream returned a null stream.");
            Assert.IsTrue(result.Length > 0, "The stream returned by MediaStreamProvider.GetMediaStream has 0 length.");
        }
    }
}