using System.IO;

namespace Oggstreamer.Providers
{
    public interface IMediaStreamProvider
    {
        Stream GetMediaStream();
    }

    public class MediaStreamProvider : IMediaStreamProvider
    {
        public Stream GetMediaStream()
        {
            var filePath = Path.Combine("assets", "test01.ogg");
            return new FileStream(filePath, FileMode.Open);
        }
    }
}