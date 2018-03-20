using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Oggstreamer.Providers
{
    public interface ITranscodingProvider
    {
        Task<string> Transcode(string originalFilePath, string targetFilePath);
    }

    public class TranscodingProvider : ITranscodingProvider
    {
        public async Task<string> Transcode(string originalFilePath, string targetFilePath)
        {
            var command = $"ffmpeg -y -i {originalFilePath} -c:a libvorbis -f oga {targetFilePath}";            
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