using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Oggstreamer.Providers
{
    public interface ITranscodingProvider
    {
        Task<string> Transcode(string originalFilePath, string targetFilePath, int audioQuality);
    }

    public class TranscodingProvider : ITranscodingProvider
    {
        public async Task<string> Transcode(string originalFilePath, string targetFilePath, int audioQuality)
        {
            if (audioQuality < 0 || audioQuality > 5) throw new ArgumentException($"{audioQuality} must be between 0 and 5.");

            var command = $"ffmpeg -y -i {originalFilePath} -c:a libvorbis -q:a {audioQuality} -f oga {targetFilePath}";            
            var process = new Process() { StartInfo = new ProcessStartInfo {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false                    
                }
            };
            process.Start();
            return await Task<string>.Factory.StartNew(() => {
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result;
            });
        }
    }
}