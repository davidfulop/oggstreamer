using System;
using System.IO;
using System.Threading.Tasks;

namespace Oggstreamer.Providers
{
    public interface IMediaStreamProvider
    {
        Task<Stream> GetMediaStream();
    }

    public class MediaStreamProvider : IMediaStreamProvider
    {
        private readonly ITranscodingProvider _transcodingProvider;

        public MediaStreamProvider(ITranscodingProvider transcodingProvider)
        {
            _transcodingProvider = transcodingProvider ?? throw new ArgumentNullException(nameof(transcodingProvider));
        }

        public async Task<Stream> GetMediaStream()
        {
            var originalFilePath = Path.Combine("assets", "test01.flac");
            var targetFilePath = Path.Combine("assets", "test01.ogg");
            await _transcodingProvider.Transcode(originalFilePath, targetFilePath);
            return new FileStream(targetFilePath, FileMode.Open);
        }
    }
}