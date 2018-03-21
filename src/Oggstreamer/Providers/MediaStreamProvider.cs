using System;
using System.IO;
using System.Threading.Tasks;

namespace Oggstreamer.Providers
{
    public interface IMediaStreamProvider
    {
        Task<Stream> GetMediaStream(int audioQuality);
    }

    public class MediaStreamProvider : IMediaStreamProvider
    {
        private readonly ITranscodingProvider _transcodingProvider;

        public MediaStreamProvider(ITranscodingProvider transcodingProvider)
        {
            _transcodingProvider = transcodingProvider ?? throw new ArgumentNullException(nameof(transcodingProvider));
        }

        public async Task<Stream> GetMediaStream(int audioQuality)
        {
            var originalFilePath = Path.Combine("assets", "test01.flac");
            var targetFilePath = Path.Combine("assets", "test01.ogg");
            await _transcodingProvider.Transcode(originalFilePath, targetFilePath, audioQuality);
            return new FileStream(targetFilePath, FileMode.Open);
        }
    }
}