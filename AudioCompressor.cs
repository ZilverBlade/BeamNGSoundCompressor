using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;

namespace BeamNGSoundCompressor
{
    public class AudioCompressor
    {
        List<Tuple<string, Task<bool>>> processingFiles = new List<Tuple<string, Task<bool>>>();
        public AudioCompressor()
        {
        }
        public async void ConvertFile(string sourceFile)
        {
            await using var audioInputStream = File.Open(sourceFile, FileMode.Open);
            await using var audioOutputStream = File.Open(Util.ReplaceFileExtention(sourceFile, "ogg"), FileMode.OpenOrCreate);

            var task = FFMpegArguments
                .FromPipeInput(new StreamPipeSource(audioInputStream))
                .OutputToPipe(new StreamPipeSink(audioOutputStream), options =>
                    options
                    .WithAudioCodec(AudioCodec.LibVorbis)
                    .WithAudioBitrate(AudioQuality.Good)
                    .ForceFormat("ogg")
                ).ProcessAsynchronously();
            processingFiles.Add(Tuple.Create(sourceFile, task));
        }
    }
}
