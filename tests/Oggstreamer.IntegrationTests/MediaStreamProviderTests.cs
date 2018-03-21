using NUnit.Framework;
using NSubstitute;
using Oggstreamer.Providers;
using System.Threading.Tasks;

namespace Oggstreamer.IntegrationTests
{
    public class MediaStreamProviderTests
    {
        private const int MIN_AUDIOQUALITY = 0;
        private const int MAX_AUDIOQUALITY = 5;
        private const int INVALID_AUDIOQUALITY = 99;
        private MediaStreamProvider _mediaStreamProvider;

        [SetUp]
        public void SetUp()
        {
            var fakeTranscodingProvider = Substitute.For<ITranscodingProvider>();
            fakeTranscodingProvider.Transcode(Arg.Any<string>(), Arg.Any<string>(), 
                Arg.Is<int>(i => i >= 0 && i <= 5)).Returns(
                    Task<string>.Factory.StartNew(() => {
                        Task.Delay(1000).Wait();
                        return "0";
                    }));
            _mediaStreamProvider = new MediaStreamProvider(fakeTranscodingProvider);
        }

        [Test]
        public async Task GetMediaStream_returns_a_stream()
        {
            var result = await _mediaStreamProvider.GetMediaStream(MIN_AUDIOQUALITY);
            Assert.IsNotNull(result, "MediaStreamProvider.GetMediaStream returned a null stream.");
            Assert.IsTrue(result.Length > 0, "The stream returned by MediaStreamProvider.GetMediaStream has 0 length.");
        }
    }
}