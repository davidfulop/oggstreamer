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
            return new FileStream("assets/test01.ogg", FileMode.Open);
        }
    }
}