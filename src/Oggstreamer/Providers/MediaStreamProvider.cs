using System;
using System.IO;

namespace Oggstreamer.Providers
{
    public interface IMediaStreamProvider
    {
        Stream GetMediaStream();
    }

    public class MediaStreamProvider : IMediaStreamProvider
    {
        private readonly ITranscodingProvider _transcodingProvider;

        public MediaStreamProvider(ITranscodingProvider transcodingProvider)
        {
            _transcodingProvider = transcodingProvider ?? throw new ArgumentNullException(nameof(transcodingProvider));
        }

        public Stream GetMediaStream()
        {
            var originalFilePath = Path.Combine("assets", "test01.flac");
            var targetFilePath = Path.Combine("assets", "test01.ogg");
            _transcodingProvider.Transcode(originalFilePath, targetFilePath).Wait();
            return new FileStream(targetFilePath, FileMode.Open);
        }
    }
}